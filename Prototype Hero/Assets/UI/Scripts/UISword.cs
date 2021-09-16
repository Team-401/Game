using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISword : MonoBehaviour
{
    public Image UIUpgradedSword;
    public bool swordIsUpgraded = false;

    void Start()
    {
        UIUpgradedSword = GetComponent<Image>();
        int pref = PlayerPrefs.GetInt("sword");
        if(pref != 0)
        {
            swordIsUpgraded = true;
        }
    }

    void Update()
    {
        if(swordIsUpgraded)
        {
            UISwordAlphaSet(1f);
        }
        else
        {
            UISwordAlphaSet(.3f);
        }
    }

    public void UISwordAlphaSet(float input)
    {
        var alpha = UIUpgradedSword.color;
        alpha.a = input;
        UIUpgradedSword.color = alpha;
    }

    public void UpgradeSword()
    {
        swordIsUpgraded = true;
    }

    public bool SwordStatus()
    {
        return swordIsUpgraded;
    }
}
