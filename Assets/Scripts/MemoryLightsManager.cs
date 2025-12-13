using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryLightsManager : MonoBehaviour
{
    public string lobbySceneName = "Lobby";
    public LightTile[] tiles;

    [Header("Timing")]
    public float flashTime = 0.35f;
    public float gapTime = 0.2f;
    public float afterSequenceDelay = 0.25f;

    [Header("Debug")]
    public bool debugLogs = true;

    private List<int> sequence = new List<int>();
    private int inputIndex = 0;
    private bool canInput = false;
    private bool isShowingSequence = false;

    void Start()
    {
        if (debugLogs) Debug.Log("MemoryLightsManager START");
        NextRound();
    }

    // called by tiles
    public void TileClicked(int tileIndex)
    {
        if (!canInput || isShowingSequence) return;

        int expected = sequence[inputIndex];

        if (debugLogs)
            Debug.Log($"CLICK: {tileIndex} | expected {expected} | inputIndex {inputIndex}/{sequence.Count-1}");

        if (tileIndex == expected)
        {
            StartCoroutine(Flash(tileIndex));
            inputIndex++;

            if (inputIndex >= sequence.Count)
            {
                canInput = false;
                Invoke(nameof(NextRound), 0.6f);
            }
        }
        else
        {
            if (debugLogs) Debug.LogWarning($"WRONG: clicked {tileIndex}, expected {expected}. Loading Lobby.");
            SceneManager.LoadScene(lobbySceneName);
        }
    }

    void NextRound()
    {
        sequence.Add(Random.Range(1, 5)); // 1..4
        inputIndex = 0;

        if (debugLogs)
            Debug.Log("NEW SEQUENCE: " + string.Join(",", sequence));

        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        canInput = false;
        isShowingSequence = true;

        // small pause before showing
        yield return new WaitForSeconds(0.25f);

        foreach (int i in sequence)
        {
            yield return Flash(i);
            yield return new WaitForSeconds(gapTime);
        }

        // wait a beat so clicks don't overlap last flash
        yield return new WaitForSeconds(afterSequenceDelay);

        isShowingSequence = false;
        canInput = true;

        if (debugLogs) Debug.Log("YOUR TURN (input enabled)");
    }

    IEnumerator Flash(int i)
    {
        int idx = i - 1;
        if (tiles == null || tiles.Length < 4 || idx < 0 || idx >= tiles.Length || tiles[idx] == null)
        {
            Debug.LogError("Tiles array not set correctly on GameManager.");
            yield break;
        }

        tiles[idx].SetLit(true);
        yield return new WaitForSeconds(flashTime);
        tiles[idx].SetLit(false);
    }
}
