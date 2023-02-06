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

    //摧毁物体
    public void Damage()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void Start()
    {
        audioSource.volume = AudioManager.instance.effectVolume;
    }

    //播放风滚草移动音效
    public void PlayTumbleweedMoveAudio()
    {
        audioSource.clip = tumbleweedMoveAudio;
        audioSource.Play();
    }

    //播放风滚草播种音效
    public void PlayTumbleweedSowAudio()
    {
        audioSource.clip = tumbleweedSowAudio;
        audioSource.Play();
    }

    //播放风滚草死亡音效
    public void PlayTumbleweedDieAudio()
    {
        audioSource.clip = tumbleweedDieAudio;
        audioSource.Play();
    }

    //播放植物生长音效
    public void PlayPlantGrowAudioAudio()
    {
        audioSource.clip = plantGrowAudio;
        audioSource.Play();
    }

    //播放植物死亡音效
    public void PlayPlantDieAudioAudio()
    {
        audioSource.clip = plantDieAudio;
        audioSource.Play();
    }

    //播放打人柳音效
    public void PlayMadWillowAudioAudio()
    {
        audioSource.clip = madWillowAudio;
        audioSource.Play();
    }

    //播放灭火树音效
    public void PlayExtinguishingTreeAudio()
    {
        audioSource.clip = extinguishingTreeAudio;
        audioSource.Play();
    }

    //播放挡火树音效
    public void PlayFireBlockerTreeAudio()
    {
        audioSource.clip = fireBlockerTreeAudio;
        audioSource.Play();
    }

    //播放动物攻击音效
    public void PlayAnimalAttackAudio()
    {
        audioSource.clip = animalAttackAudio;
        audioSource.Play();
    }

    //播放动物死亡音效
    public void PlayAnimalDieAudio()
    {
        audioSource.clip = animalDieAudio;
        audioSource.Play();
    }

    //播放挖掘机攻击音效
    public void PlayExcavatorAttackAudio()
    {
        audioSource.clip = excavatorAttackAudio;
        audioSource.Play();
    }

    //播放挖掘机死亡音效
    public void PlayExcavatorDieAudio()
    {
        audioSource.clip = excavatorDieAudio;
        audioSource.Play();
    }
}
