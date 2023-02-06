using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public float randomNum;


    void Start()
    {
        randomPlay();
    }


    void Update()
    {

    }

    void randomPlay()
    {
        randomNum = Random.Range(1.0f, 5.0f);
        if (randomNum >= 1.0f && randomNum < 2.0f) 
        { 
            audioSource.clip = audioClip[0]; 
            audioSource.Play(); 
        }
        else if (randomNum >= 2.0f && randomNum < 3.0f) 
        { 
            audioSource.clip = audioClip[1]; 
            audioSource.Play(); 
        }
        else if (randomNum >= 3.0f && randomNum < 4.0f) 
        { 
            audioSource.clip = audioClip[2]; 
            audioSource.Play(); 
        }
        else if (randomNum >= 4.0f && randomNum <= 5.0f)
        {
            audioSource.clip = audioClip[3];
            audioSource.Play();
        }
    }
}
