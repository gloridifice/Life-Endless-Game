using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2023.Audio;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip tumbleweedMoveAudio;
    public AudioClip tumbleweedSowAudio;
    public AudioClip tumbleweedDieAudio;

    public AudioClip plantGrowAudio;
    public AudioClip plantDieAudio;

    public AudioClip madWillowAudio;
    public AudioClip extinguishingTreeAudio;
    public AudioClip fireBlockerTreeAudio;

    public AudioClip animalAttackAudio;
    public AudioClip animalDieAudio;

    public AudioClip excavatorAttackAudio;
    public AudioClip excavatorDieAudio;

    //???????
    public void Damage()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void Start()
    {
        
    }

    //?????????????完
    public void PlayTumbleweedMoveAudio()
    {
        audioSource.clip = tumbleweedMoveAudio;
        audioSource.Play();
    }

    //?????????????完
    public void PlayTumbleweedSowAudio()
    {
        audioSource.clip = tumbleweedSowAudio;
        audioSource.Play();
    }

    //??????????????完
    public void PlayTumbleweedDieAudio()
    {
        audioSource.clip = tumbleweedDieAudio;
        audioSource.Play();
    }

    //?????????????完
    public void PlayPlantGrowAudioAudio()
    {
        audioSource.clip = plantGrowAudio;
        audioSource.Play();
    }

    //?????????????完
    public void PlayPlantDieAudioAudio()
    {
        audioSource.clip = plantDieAudio;
        audioSource.Play();
    }

    //???????????完
    public void PlayMadWillowAudioAudio()
    {
        audioSource.clip = madWillowAudio;
        audioSource.Play();
    }

    //???????????完
    public void PlayExtinguishingTreeAudio()
    {
        audioSource.clip = extinguishingTreeAudio;
        audioSource.Play();
    }

    //???????????完
    public void PlayFireBlockerTreeAudio()
    {
        audioSource.clip = fireBlockerTreeAudio;
        audioSource.Play();
    }

    //??????��????完
    public void PlayAnimalAttackAudio()
    {
        audioSource.clip = animalAttackAudio;
        audioSource.Play();
    }

    //?????????????完
    public void PlayAnimalDieAudio()
    {
        audioSource.clip = animalDieAudio;
        audioSource.Play();
    }

    //??????????????完
    public void PlayExcavatorAttackAudio()
    {
        audioSource.clip = excavatorAttackAudio;
        audioSource.Play();
    }

    //??????????????完
    public void PlayExcavatorDieAudio()
    {
        audioSource.clip = excavatorDieAudio;
        audioSource.Play();
    }
}
