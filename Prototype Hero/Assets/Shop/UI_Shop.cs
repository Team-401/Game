using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopBox;

    public Button BuySwordButton;
    public Button BuyCharmButton;
    public Button BuyPotionButton;

    public int moneyAmount;

    private void Start()
    {
        moneyAmount = 500;
        Close();
    }
    public void Update()
    {
        
    }
    public void Open()
    {
        shopBox.SetActive(true);
    }
    public void Close()
    {
        shopBox.SetActive(false);
    }

    public void BuyPotion()
    {
        if(moneyAmount >= 25)
        {
            moneyAmount = moneyAmount - 25;
            Debug.Log("Potion purchased, money: " + moneyAmount);
        }
        else
        {
         Text text =  BuyPotionButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
    public void BuyCharm()
    {
        if (moneyAmount >= 200)
        {
            moneyAmount = moneyAmount - 200;
            Debug.Log("Charm purchased, money: " + moneyAmount);
        }
        else
        {
            Text text = BuyCharmButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
    public void BuySword()
    {
        if (moneyAmount >= 250)
        {
            moneyAmount = moneyAmount - 250;
            Debug.Log("Sword purchased, money: " + moneyAmount);
        }
        else
        {
            Text text = BuySwordButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
}
