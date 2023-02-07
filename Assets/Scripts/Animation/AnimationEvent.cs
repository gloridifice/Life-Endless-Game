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

    //?????????????��
    public void PlayTumbleweedMoveAudio()
    {
        audioSource.clip = tumbleweedMoveAudio;
        audioSource.Play();
    }

    //?????????????��
    public void PlayTumbleweedSowAudio()
    {
        audioSource.clip = tumbleweedSowAudio;
        audioSource.Play();
    }

    //??????????????��
    public void PlayTumbleweedDieAudio()
    {
        audioSource.clip = tumbleweedDieAudio;
        audioSource.Play();
    }

    //?????????????��
    public void PlayPlantGrowAudioAudio()
    {
        audioSource.clip = plantGrowAudio;
        audioSource.Play();
    }

    //?????????????��
    public void PlayPlantDieAudioAudio()
    {
        audioSource.clip = plantDieAudio;
        audioSource.Play();
    }

    //???????????��
    public void PlayMadWillowAudioAudio()
    {
        audioSource.clip = madWillowAudio;
        audioSource.Play();
    }

    //???????????��
    public void PlayExtinguishingTreeAudio()
    {
        audioSource.clip = extinguishingTreeAudio;
        audioSource.Play();
    }

    //???????????��
    public void PlayFireBlockerTreeAudio()
    {
        audioSource.clip = fireBlockerTreeAudio;
        audioSource.Play();
    }

    //??????��????��
    public void PlayAnimalAttackAudio()
    {
        audioSource.clip = animalAttackAudio;
        audioSource.Play();
    }

    //?????????????��
    public void PlayAnimalDieAudio()
    {
        audioSource.clip = animalDieAudio;
        audioSource.Play();
    }

    //??????????????��
    public void PlayExcavatorAttackAudio()
    {
        audioSource.clip = excavatorAttackAudio;
        audioSource.Play();
    }

    //??????????????��
    public void PlayExcavatorDieAudio()
    {
        audioSource.clip = excavatorDieAudio;
        audioSource.Play();
    }
}
