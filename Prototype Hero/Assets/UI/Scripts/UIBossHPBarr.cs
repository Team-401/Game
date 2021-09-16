using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHPBarr : MonoBehaviour
{
    public Gradient gradient;
    public Slider slider;
    public Image fill;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBossMaxHealth(int maxHealth)
    {
        slider.value = maxHealth;
        slider.maxValue = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetBossHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
