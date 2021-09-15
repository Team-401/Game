using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCharm : MonoBehaviour
{
    public Image UICharm;
    public bool hasCharm = false;

    // Start is called before the first frame update
    void Start()
    {
        UICharm = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCharm == true)
        {
            UICharmAlphaUp(1f);
        }
        else
        {
            UICharmAlphaDown(.5f);
        }
    }
    public void UICharmAlphaUp(float input)
    {
        var alpha = UICharm.color;
        alpha.a = input;
        UICharm.color = alpha;
    }

    public void UICharmAlphaDown(float input)
    {
        var alpha = UICharm.color;
        alpha.a = input;
        UICharm.color = alpha;
    }

    public void HasCharm()
    {
        hasCharm = true;
    }
}
