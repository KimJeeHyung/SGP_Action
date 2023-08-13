using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTitle : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.StopAllCoroutines();
        SoundManager.instance.StartCoroutine("PlayBGM", "Title");
    }
}
