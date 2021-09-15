using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PrototypeHero player;
    [SerializeField] private UI_Shop shopUI;
    
    public IInteractable Interactable { get; set; }
    public IShop Shop { get; set; }

    public DialogueUI DialogueUI => dialogueUI;
    public UI_Shop UI_Shop => shopUI;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Interactable?.Interact(dialogueUI: dialogueUI);
            Shop?.Interact(shopUI: shopUI);
        }
    }
}
