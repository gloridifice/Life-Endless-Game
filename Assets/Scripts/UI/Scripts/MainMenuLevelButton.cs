using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GGJ2023;
using GGJ2023.Audio;
using TMPro;
using UnityEngine;

public class MainMenuLevelButton : MonoBehaviour
{
    [HideInInspector] public int level;

    [HideInInspector] public string levelIndexName;

    public Transform circle0;
    public Transform circle1;
    private TMP_Text countText;

    private Tweener inTwn, outTwn;
    public TMP_Text CountText
    {
        get
        {
            if (countText == null)
            {
                countText = GetComponentInChildren<TMP_Text>();
            }

            return countText;
        }
    }

    private void Start()
    {
        inTwn = circle0.DOScale(1.3f * Vector3.one, 0.6f);
        outTwn = circle0.DOScale(1.0f * Vector3.one, 0.4f);
        inTwn.SetAutoKill(false);
        outTwn.SetAutoKill(false);
        inTwn.SetEase(Ease.OutBounce);
        outTwn.SetEase(Ease.InOutQuart);
        inTwn.Pause();
        outTwn.Pause();
        
    }

    private void OnMouseDown()
    {
        GameManager.Instance.LoadLevel(level);
    }

    private void OnMouseEnter()
    {
        AudioManager.Instance.Play("effect","click");
        outTwn.Pause();
        inTwn.Restart();
    }

    private void OnMouseExit()
    {
        inTwn.Pause();
        outTwn.Restart();
    }
}
