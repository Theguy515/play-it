using UnityEngine;

public class LightTile : MonoBehaviour
{
    public int tileIndex = 1;
    public Color baseColor = Color.gray;
    public Color litColor = Color.white;

    private SpriteRenderer sr;
    private MemoryLightsManager manager;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<MemoryLightsManager>();
        SetLit(false);
    }

    public void SetLit(bool lit)
    {
        sr.color = lit ? litColor : baseColor;
    }

    void OnMouseDown()
    {
        // When clicked, tell the manager which tile was clicked
        if (manager != null)
        {
            manager.TileClicked(tileIndex);
        }
    }
}
