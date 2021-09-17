using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCharm : MonoBehaviour
{
    public Image UICharm;
    public bool hasCharm = false;

    void Start()
    {
        UICharm = GetComponent<Image>();
        int pref = PlayerPrefs.GetInt("charm");
        if (pref != 0)
        {
            hasCharm = true;
        }
    }

    void Update()
    {
        if (hasCharm == true)
        {
            UICharmAlphaSet(1f);
        }
        else
        {
            UICharmAlphaSet(.3f);
        }
    }
    public void UICharmAlphaSet(float input)
    {
        var alpha = UICharm.color;
        alpha.a = input;
        UICharm.color = alpha;
    }

    public void GetCharm()
    {
        hasCharm = true;
    }
    public bool HasCharm()
    {
        return hasCharm;
    }
}
