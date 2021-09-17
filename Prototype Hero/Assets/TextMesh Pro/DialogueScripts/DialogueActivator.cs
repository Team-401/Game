
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void Interact(DialogueUI dialogueUI)
    {
        dialogueUI.ShowDialogue(dialogueObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Interactions interactions))
        {
            interactions.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Interactions interactions))
        {
            interactions.Interactable = null;
        }

    }
}
