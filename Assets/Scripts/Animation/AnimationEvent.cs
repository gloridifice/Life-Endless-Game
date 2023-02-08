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

    public bool canPlayfireBlockerTreeAudio = false;

    //???????
    public void Damage()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void Start()
    {
        
    }

    //?????????????Ч
    public void PlayTumbleweedMoveAudio()
    {
        //audioSource.clip = tumbleweedMoveAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "tumbleweed_move");
    }

    //?????????????Ч
    public void PlayTumbleweedSowAudio()
    {
        //audioSource.clip = tumbleweedSowAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "tumbleweed_sow");
    }

    //??????????????Ч
    public void PlayTumbleweedDieAudio()
    {
        //audioSource.clip = tumbleweedDieAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "tumbleweed_die");
    }

    //?????????????Ч
    public void PlayPlantGrowAudioAudio()
    {
        //audioSource.clip = plantGrowAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "plant_grow");
    }

    //?????????????Ч
    public void PlayPlantDieAudioAudio()
    {
        //audioSource.clip = plantDieAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "plant_die");
    }

    //???????????Ч
    public void PlayMadWillowAudio()
    {
        //audioSource.clip = madWillowAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "mad_willow");
    }

    //???????????Ч
    public void PlayExtinguishingTreeAudio()
    {
        //audioSource.clip = extinguishingTreeAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "extinguishing_tree");
    }

    //???????????Ч
    public void PlayFireBlockerTreeAudio()
    {
        if(canPlayfireBlockerTreeAudio)
        {
            //audioSource.clip = fireBlockerTreeAudio;
            //audioSource.Play();
            AudioManager.Instance.Play("effect", "fire_blocker_tree");
            canPlayfireBlockerTreeAudio = false;
        }
    }

    //??????﹥????Ч
    public void PlayAnimalAttackAudio()
    {
        //audioSource.clip = animalAttackAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "animal_attack");
    }

    //?????????????Ч
    public void PlayAnimalDieAudio()
    {
        //audioSource.clip = animalDieAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "animal_die");
    }

    //??????????????Ч
    public void PlayExcavatorAttackAudio()
    {
        //audioSource.clip = excavatorAttackAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "excavator_attack");
    }

    //??????????????Ч
    public void PlayExcavatorDieAudio()
    {
        //audioSource.clip = excavatorDieAudio;
        //audioSource.Play();
        AudioManager.Instance.Play("effect", "excavator_die");
    }
}
