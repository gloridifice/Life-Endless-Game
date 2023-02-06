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

    //�ݻ�����
    public void Damage()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void Start()
    {
        audioSource.volume = AudioManager.instance.effectVolume;
    }

    //���ŷ�����ƶ���Ч
    public void PlayTumbleweedMoveAudio()
    {
        audioSource.clip = tumbleweedMoveAudio;
        audioSource.Play();
    }

    //���ŷ���ݲ�����Ч
    public void PlayTumbleweedSowAudio()
    {
        audioSource.clip = tumbleweedSowAudio;
        audioSource.Play();
    }

    //���ŷ����������Ч
    public void PlayTumbleweedDieAudio()
    {
        audioSource.clip = tumbleweedDieAudio;
        audioSource.Play();
    }

    //����ֲ��������Ч
    public void PlayPlantGrowAudioAudio()
    {
        audioSource.clip = plantGrowAudio;
        audioSource.Play();
    }

    //����ֲ��������Ч
    public void PlayPlantDieAudioAudio()
    {
        audioSource.clip = plantDieAudio;
        audioSource.Play();
    }

    //���Ŵ�������Ч
    public void PlayMadWillowAudioAudio()
    {
        audioSource.clip = madWillowAudio;
        audioSource.Play();
    }

    //�����������Ч
    public void PlayExtinguishingTreeAudio()
    {
        audioSource.clip = extinguishingTreeAudio;
        audioSource.Play();
    }

    //���ŵ�������Ч
    public void PlayFireBlockerTreeAudio()
    {
        audioSource.clip = fireBlockerTreeAudio;
        audioSource.Play();
    }

    //���Ŷ��﹥����Ч
    public void PlayAnimalAttackAudio()
    {
        audioSource.clip = animalAttackAudio;
        audioSource.Play();
    }

    //���Ŷ���������Ч
    public void PlayAnimalDieAudio()
    {
        audioSource.clip = animalDieAudio;
        audioSource.Play();
    }

    //�����ھ��������Ч
    public void PlayExcavatorAttackAudio()
    {
        audioSource.clip = excavatorAttackAudio;
        audioSource.Play();
    }

    //�����ھ��������Ч
    public void PlayExcavatorDieAudio()
    {
        audioSource.clip = excavatorDieAudio;
        audioSource.Play();
    }
}
