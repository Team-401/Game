
using UnityEngine;

public class MattDialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private float speed;
    private int distance;
    private Vector3 direction;
    void Start()
    {
        distance = 0;
        direction = new Vector3(speed,0,0);
    }

    void Update() 
    {
        if(distance>0)
        {
            transform.position = transform.position + direction;
            distance--;
        }
    }

    public void Interact(DialogueUI dialogueUI)
    {
        dialogueUI.ShowDialogue(dialogueObject);
        distance = 100;
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
