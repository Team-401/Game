using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIPotion : MonoBehaviour
{
    public TextMeshProUGUI potionText;
    public int potionCount;
    // Start is called before the first frame update
    void Start()
    {
        potionText = GetComponent<TextMeshProUGUI>();
        potionCount = PlayerPrefs.GetInt("potions");
    }

    // Update is called once per frame
    void Update()
    {
        potionText.text = $"x{potionCount.ToString()}";
        if(Input.GetKeyDown(KeyCode.T)) { increasePotion(); }
    }

    public void increasePotion()
    {
        if (potionCount + 1 >= 3)
        {
            potionCount = 3;
        }
        else
        {
            potionCount++;
        }
    }

    public void decreasePotion()
    {
        potionCount--;
    }
}
