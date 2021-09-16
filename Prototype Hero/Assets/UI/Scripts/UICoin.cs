using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UICoin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coinCount;
    public bool canBuyItem;

    private void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        coinCount = PlayerPrefs.GetInt("coins");
    }

    private void Update()
    {
        coinText.text = $"x{coinCount.ToString()}";
        if (Input.GetKeyDown(KeyCode.Y)) { increaseCoins(33); }
        if (coinCount >= 33)
        {
            canBuyItem = true;
        }
        else { canBuyItem = false; }
    }

    public void BanditLoot()
    {
        increaseCoins(60);
    }

    public void increaseCoins(int coins)
    {
        if (coinCount + coins >= 99)
        {
            coinCount = 99;
        }
        else
        {
            coinCount += coins;
        }
    }

    public void decreaseCoins(int coins)
    {
        coinCount -= coins;
    }

    public int Count()
    {
        return coinCount;
    }
}
