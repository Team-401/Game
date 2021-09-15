using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISword : MonoBehaviour
{
    public Image UIUpgradedSword;
    public bool swordIsUpgraded = false;

    // Start is called before the first frame update
    void Start()
    {
        UIUpgradedSword = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(swordIsUpgraded == true)
        {
            UISwordAlphaUp(1f);
        }
        else
        {
            UISwordAlphaDown(.5f);
        }
    }

    public void UISwordAlphaUp(float input)
    {
        var alpha = UIUpgradedSword.color;
        alpha.a = input;
        UIUpgradedSword.color = alpha;
    }

    public void UISwordAlphaDown(float input)
    {
        var alpha = UIUpgradedSword.color;
        alpha.a = input;
        UIUpgradedSword.color = alpha;
    }

    public void UpgradeSword()
    {
        swordIsUpgraded = true;
    }
}
