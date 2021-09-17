using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopBox;
    [SerializeField] private UICoin coinUI;
    [SerializeField] private UIPotion potionUI;
    [SerializeField] private UISword swordUI;
    [SerializeField] private UiCharm charmUI;
    public Button BuySwordButton;
    public Button BuyCharmButton;
    public Button BuyPotionButton;

    private int moneyAmount;
    public bool IsOpen;

    private void Start()
    {
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

        moneyAmount = coinUI.Count();

        IsOpen = true;
        Cursor.visible = true;
        shopBox.SetActive(true);
    }
    public void Close()
    {
        IsOpen = false;
        Cursor.visible = false;
        shopBox.SetActive(false);
    }

    public void BuyPotion()
    {
        if(potionUI.potionCount >= 3)
        {
            Text text = BuyPotionButton.GetComponentInChildren<Text>();
            text.text = "Too many!";
        }
        else if(moneyAmount >= 10)
        {
            moneyAmount = moneyAmount - 10;
            Debug.Log("Potion purchased, money: " + moneyAmount);
            coinUI.decreaseCoins(10);
            potionUI.increasePotion();
        }
        else
        {
         Text text =  BuyPotionButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
    public void BuyCharm()
    {
        if(charmUI.HasCharm())
        {
            Text text = BuyCharmButton.GetComponentInChildren<Text>();
            text.text = "Already bought!";
        }
        if (moneyAmount >= 20)
        {
            if(true)
            {
                moneyAmount = moneyAmount - 20;
                Debug.Log("Charm purchased, money: " + moneyAmount);
                coinUI.decreaseCoins(20);
                charmUI.GetCharm();
            }
        }
        else
        {
            Text text = BuyCharmButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
    public void BuySword()
    {
        if(swordUI.SwordStatus())
        {
            Text text = BuySwordButton.GetComponentInChildren<Text>();
            text.text = "Already bought!";
        }
        else if (moneyAmount >= 25)
        {
            moneyAmount = moneyAmount - 25;
            Debug.Log("Sword purchased, money: " + moneyAmount);
            coinUI.decreaseCoins(25);
            swordUI.UpgradeSword();
        }
        else
        {
            Text text = BuySwordButton.GetComponentInChildren<Text>();
            text.text = "Not enough coins!";
        }

    }
}
