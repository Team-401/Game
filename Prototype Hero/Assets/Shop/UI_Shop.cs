using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopBox;

    private void Start()
    {
        Close();
    }
    public void Open()
    {
        shopBox.SetActive(true);
    }
    public void Close()
    {
        shopBox.SetActive(false);
    }
}
