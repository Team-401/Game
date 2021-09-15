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
        Text textP = BuyPotionButton.GetComponentInChildren<Text>();
        textP.text = "Health Potion";

        Text textC = BuyCharmButton.GetComponentInChildren<Text>();
        textC.text = "Charm";

        Text textS = BuySwordButton.GetComponentInChildren<Text>();
        textS.text = "Steel Sword";

        shopBox.SetActive(true);
    }
    public void Close()
    {
        shopBox.SetActive(false);
    }

    public void BuyPotion()
    {
        if(moneyAmount >= 10)
        {
            moneyAmount = moneyAmount - 10;
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
        if (moneyAmount >= 20)
        {
            moneyAmount = moneyAmount - 20;
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
        if (moneyAmount >= 25)
        {
            moneyAmount = moneyAmount - 25;
            Debug.Log("Sword purchased, money: " + moneyAmount);
        }
        else
        {
            Text text = BuySwordButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
}
