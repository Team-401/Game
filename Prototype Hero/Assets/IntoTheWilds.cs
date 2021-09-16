using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class IntoTheWilds : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;

    [SerializeField] private UICoin coinUI;
    [SerializeField] private UIPotion potionUI;
    [SerializeField] private UISword swordUI;
    [SerializeField] private UiCharm charmUI;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("coins", coinUI.Count());
            PlayerPrefs.SetInt("potions", potionUI.potionCount);
            if(charmUI.HasCharm())
            {
                PlayerPrefs.SetInt("charm", 1);
            }
            else
            {
                PlayerPrefs.SetInt("charm", 0);
            }
            if (swordUI.SwordStatus())
            {
                PlayerPrefs.SetInt("sword", 1);
            }
            else
            {
                PlayerPrefs.SetInt("sword", 0);
            }
            Debug.Log($"coins: {PlayerPrefs.GetInt("coins")}");
            Debug.Log($"potions: {PlayerPrefs.GetInt("potions")}");
            Debug.Log($"sword: {PlayerPrefs.GetInt("sword")}");
            Debug.Log($"charm: {PlayerPrefs.GetInt("charm")}");


            //GameState.isComingFromForest = true;
            SceneManager.LoadScene(2);
        }
    }
}
