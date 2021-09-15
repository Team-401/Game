using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UICoin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public static int coinCount;
    public static bool canBuyItem;

    private void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
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

    public static void BanditLoot()
    {
        increaseCoins(60);
    }

    public static void increaseCoins(int coins)
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

    public static void decreaseCoins(int coins)
    {
        coinCount -= coins;
    }
}
