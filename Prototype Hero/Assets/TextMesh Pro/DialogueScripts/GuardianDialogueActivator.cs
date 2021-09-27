using UnityEngine;

public class GuardianDialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject1;
    [SerializeField] private DialogueObject dialogueObject2;
    private DialogueObject dialogueObject;


    void Start()
    {
        if (PlayerPrefs.GetInt("charm") == 1)
        {
            dialogueObject = dialogueObject2;
        }
        else
        {
            dialogueObject = dialogueObject1;
        }
    }

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
