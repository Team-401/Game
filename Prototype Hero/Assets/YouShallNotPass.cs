using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouShallNotPass : MonoBehaviour
{
    public GameObject bossWall;
    public UiCharm charmUI;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("charm") == 1)
        {
            bossWall.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
