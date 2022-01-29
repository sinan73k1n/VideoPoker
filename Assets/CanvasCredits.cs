using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCredits : MonoBehaviour
{

    [SerializeField] Button _btnDaily, _btnAds, _btnBack;

    
    void Start()
    {
        _btnDaily.onClick.AddListener(() => HandleDaily());
        _btnAds.onClick.AddListener(() => HandleAds());
        _btnBack.onClick.AddListener(() => HandleBack());

    }

    private void HandleBack()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        AdControl.instance.CloseBanner();
        Destroy(gameObject);
    }

    private void HandleAds()
    {
        
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        AdControl.instance.ShowRewardedVideo();
    }
    private void HandleDaily()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        GameManagerVideoPoker.instance.AddCredits(10);
    }


}
