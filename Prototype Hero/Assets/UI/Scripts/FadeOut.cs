using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public AudioSource audioSource;
    public float duration;
    public float targetVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOutMethod()
    {
        StartCoroutine(MusicScript.StartFade( audioSource,  duration, targetVolume));
    }
}
