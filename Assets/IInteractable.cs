using UnityEngine;   // <-- needed so GameObject resolves

public interface IInteractable
{
    string Prompt { get; }
    void Interact(GameObject interactor);

    // keep this only if you want it (you already added it in ArcadeCabinet)
    bool CanInteract(GameObject interactor);
}