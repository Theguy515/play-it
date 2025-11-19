using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] CanvasGroup promptUI;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] Key key = Key.E;

    IInteractable current;

    void Awake() { Show(false); }

    void Update()
    {
        if (current != null && Keyboard.current != null && Keyboard.current[key].wasPressedThisFrame)
            current.Interact(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var i = other.GetComponentInParent<IInteractable>();
        if(i != null)
        {
            current = i;
            if (promptText != null)
                promptText.text = (i is ArcadeCabinet ac) ? ac.Prompt : "Press E";
            if (i is ArcadeCabinet cab)
                cab.SetHighlighted(true);

            Show(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        var i = other.GetComponentInParent<IInteractable>();
        if(i != null && i == current)
        {
            if (current is ArcadeCabinet cab)
                cab.SetHighlighted(false);
            current = null;
            Show(false);
        }
    }

    void Show(bool v)
    {
        if (promptUI != null)
        {
            promptUI.alpha = v ? 1f : 0f;
            promptUI.blocksRaycasts = v;
            promptUI.interactable = v;
        }
    }
    
}