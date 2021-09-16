using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        float volFloat = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MusicVol", volFloat);
        PlayerPrefs.SetFloat("MusicVol", volFloat);
        Debug.Log(volFloat);
    }
}