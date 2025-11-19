using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class ArcadeCabinet : MonoBehaviour, IInteractable
{

    [TextArea]
    [SerializeField] private string prompt = "Press E to interact";
    [SerializeField] private string gameSceneName = "";

    [Header("InteractIcon")]
    [SerializeField] private SpriteRenderer interactIcon;
    [SerializeField] private Vector2 iconLocalOffset = new Vector2(0f, -0.6f);
    [SerializeField] private UnityEvent onInteract;

    public string Prompt => prompt;

    public void Interact(GameObject interactor)
    {
        //Invokes any actions that are hooked up
        onInteract?.Invoke();

        //option that loads a minigame scene if provided
        if (!string.IsNullOrWhiteSpace(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }

    }

    public bool CanInteract(GameObject interactor)
    {
        // Always interactable for now; add distance/lock logic later if needed
        return true;
    }

    private void Awake()
    {
        //keep icon hidden and in right place
        if (interactIcon != null)
        {
            interactIcon.enabled = false;
            interactIcon.transform.localPosition = (Vector3)iconLocalOffset;
        }
    }
    private void OnValidate()
    {
        if (interactIcon != null)
            interactIcon.transform.localPosition = (Vector3)iconLocalOffset;
    }
    public void SetHighlighted(bool on)
    {
        if (interactIcon != null)
            interactIcon.enabled = on;
    }
    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }
    
    
    // Update is called once per frame
    void Update()
    {

    }
}
