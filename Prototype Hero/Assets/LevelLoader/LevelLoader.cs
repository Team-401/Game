using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // If adding more scenes this will break. Have to change the values if more than 3 scenes >>>>not index 3<<<<
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        if(levelIndex >=3)
        {
            levelIndex = 0;
        }

        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("potions", 0);
        PlayerPrefs.SetInt("charm", 0);
        PlayerPrefs.SetInt("sword", 0);


        SceneManager.LoadScene(levelIndex);


    }
}
