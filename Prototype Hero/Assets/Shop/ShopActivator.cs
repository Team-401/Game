using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour, IShop
{
    public void Interact(UI_Shop shopUI)
    {
        shopUI.Open();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Interactions interactions))
        {
            interactions.Shop= this;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Interactions interactions))
        {
            interactions.Shop = null;
        }

    }
}
