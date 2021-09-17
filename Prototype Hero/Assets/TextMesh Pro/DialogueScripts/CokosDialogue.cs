using UnityEngine;
using UnityEngine.UI;

public class CokosDialogue : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject1;
    [SerializeField] private DialogueObject dialogueObject2;

    public void Interact(DialogueUI dialogueUI)
    {
        if (PlayerPrefs.GetString("cokos") == "yes")
        {
            dialogueUI.ShowDialogue(dialogueObject2);
        }
        else
        {
            PlayerPrefs.SetString("cokos", "yes");
            dialogueUI.ShowDialogue(dialogueObject1);
        }
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
