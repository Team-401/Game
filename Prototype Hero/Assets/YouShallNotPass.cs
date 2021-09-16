using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouShallNotPass : MonoBehaviour
{
    public GameObject magicBarrier;
    public UiCharm charmUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(charmUI.HasCharm())
        {
            magicBarrier.SetActive(false);
        }
    }
}
