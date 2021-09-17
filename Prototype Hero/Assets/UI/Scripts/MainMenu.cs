using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("potions", 0);
        PlayerPrefs.SetInt("charm",0);
        PlayerPrefs.SetInt("sword", 0);

        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        
        SceneManager.LoadScene(0);
    }

    public void ToCredits()
    {

        SceneManager.LoadScene(3);
    }
}

