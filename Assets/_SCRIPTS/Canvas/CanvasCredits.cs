using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasCredits : MonoBehaviour
{
    public static CanvasCredits instance;

    [SerializeField] Button _btnDaily, _btnBack;
    bool isReadyForDaily    = true;
    public bool isReadyForDailyAds = true;
    public int countAds     = 0;
    TMP_Text _txtDaily;

    void Awake()
    {
        instance = this;
        GetComponent<Canvas>().sortingOrder = 10;
        countAds           = KAYIT.GetDAILY_CREDIT_REKLAM_COUNT();
        isReadyForDailyAds = Kontrol15DakikalikAds();
        _txtDaily          = _btnDaily.GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        _btnDaily.onClick.AddListener(() => HandleDaily());
        _btnBack.onClick.AddListener(() => HandleBack());
        CheckTheDaily();
        CheckTheDailyAds();
    }

    void Update()
    {
        GeriSayimlar();
    }

    void CheckTheDaily()
    {
        if (KontrolGunluk())
        {
            _btnDaily.interactable = true;
            isReadyForDaily        = true;
            _txtDaily.text         = "DAILY +50";
        }
        else
        {
            _btnDaily.interactable = false;
            isReadyForDaily        = false;
        }
    }

    public void CheckTheDailyAds()
    {
        if (KAYIT.GetDAILY_CREDIT_REKLAM().Date < DateTime.Now.Date)
        {
            countAds = 5;
            KAYIT.SetDAILY_CREDIT_REKLAM_COUNT(countAds);
            KAYIT.SetDAILY_CREDIT_REKLAM(DateTime.Now);
            isReadyForDailyAds = true;
        }

        if (countAds <= 0)
            isReadyForDailyAds = false;
    }

    bool KontrolGunluk()       => KAYIT.GetDAILY_CREDIT_LAST_TIME().Date < DateTime.Now.Date;
    bool KontrolGunlukAds()    => KAYIT.GetDAILY_CREDIT_REKLAM().Date    < DateTime.Now.Date;
    bool Kontrol15DakikalikAds() => KAYIT.GetDAILY_CREDIT_REKLAM_15DK().TimeOfDay < DateTime.Now.TimeOfDay;

    void GeriSayimlar()
    {
        if (!isReadyForDaily)
        {
            _txtDaily.text = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
            if (KontrolGunluk()) CheckTheDaily();
        }

        if (countAds <= 0 && KontrolGunlukAds())
            CheckTheDailyAds();
    }

    void HandleBack()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }

    public void ReklamComplete()
    {
        GameManagerVideoPoker.instance.AddCredits(25);
        KAYIT.SetDAILY_CREDIT_REKLAM_15DK(DateTime.Now.AddMinutes(15));
        countAds--;
        KAYIT.SetDAILY_CREDIT_REKLAM_COUNT(countAds);
        isReadyForDailyAds = false;
        if (!GameManagerVideoPoker.instance.isAdim2) GameManagerVideoPoker.instance.CheckBetAndCredit();
        CheckTheDailyAds();
    }

    void HandleDaily()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        GameManagerVideoPoker.instance.AddCredits(50);
        KAYIT.SetDAILY_CREDIT_LAST_TIME(DateTime.Now);
        CheckTheDaily();
    }
}
