using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowBossHPBar : MonoBehaviour
{
    public GameObject bossHealthBar;
    private bool _showHealthBar = false;

    // Start is called before the first frame update
    void Start()
    {
        //bossHealthBar = GetComponent<GameObject>();
        Debug.Log($"ShowBossHPBAr: {_showHealthBar}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) 
        {
            _showHealthBar = true;
            Debug.Log($"ShowBossHPBAr: {_showHealthBar}");
        }
        if (Input.GetKeyDown(KeyCode.B)) 
        { 
            _showHealthBar = false;
            Debug.Log($"ShowBossHPBAr: {_showHealthBar}");
        }
        if (_showHealthBar == true)
        {
            bossHealthBar.SetActive(true);
        }
        else
        {
            bossHealthBar.SetActive(false);
        }
    }

    public void showBossHealthBar()
    {
        _showHealthBar = true;
    }

    public void HideBossHealthBar()
    {
        _showHealthBar = false;
    }
}
