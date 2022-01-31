using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasCredits : MonoBehaviour
{
    public static CanvasCredits instance;
    [SerializeField] Button _btnDaily, _btnAds, _btnBack;
    bool isReadyForDaily = true;
   public bool isReadyForDailyAds = true;
    public int countAds = 0;
    TMP_Text _txtDaily, _txtAds;
    private void Awake()
    {
        instance = this;
        GetComponent<Canvas>().sortingOrder = 10;
        countAds = KAYIT.GetDAILY_CREDIT_REKLAM_COUNT();
        isReadyForDailyAds = Kontrol15DakikalikAds();
        Debug.Log(countAds);
        _txtDaily = _btnDaily.GetComponentInChildren<TMP_Text>();
        _txtAds = _btnAds.GetComponentInChildren<TMP_Text>();

    }
    void Start()
    {
        _btnDaily.onClick.AddListener(() => HandleDaily());
        _btnAds.onClick.AddListener(() => HandleAds());
        _btnBack.onClick.AddListener(() => HandleBack());
        CheckTheDaily();
        CheckTheDailyAds();


        //Debug.Log(DateTime.Now);
        //Debug.Log(DateTime.Now.AddMinutes(15));
        //string a = DateTime.Now.ToString();
        //Debug.Log(a);
        //DateTime b = DateTime.Parse(a);
        //Debug.Log(b);
        //Debug.Log("---------");



    }
    private void Update()
    {
        GeriSayimlar();
        //DateTime dateTime = KAYIT.GetDAILY_CREDIT_LAST_TIME();
        //Debug.Log(((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8));

    }

    void CheckTheDaily()
    {


        if (KontrolGunluk())
        {
            _btnDaily.interactable = true;
            isReadyForDaily = true;
            _txtDaily.text = "DAILY +50";

        }
        else
        {
            _btnDaily.interactable = false;
            isReadyForDaily = false;
        }
    }
  public  void CheckTheDailyAds()
    {
        if (KAYIT.GetDAILY_CREDIT_REKLAM().Date < DateTime.Now.Date)
        {
            countAds = 5;
            KAYIT.SetDAILY_CREDIT_REKLAM_COUNT(countAds);
            KAYIT.SetDAILY_CREDIT_REKLAM(DateTime.Now);
            isReadyForDailyAds = true;
            _btnAds.interactable = true;
        }

        if (countAds==5)
        {
            _btnAds.interactable = true;
     
            _txtAds.text = $"ADS({countAds}) +50";

        }
        else if (countAds>0&& isReadyForDailyAds)
        {
            _btnAds.interactable = true;
            _txtAds.text = $"ADS({countAds}) +50";
        }
        else
        {
            _btnAds.interactable = false;
            isReadyForDailyAds = false;
  
        }
    }

    bool KontrolGunluk()
    {
        DateTime dateTime = KAYIT.GetDAILY_CREDIT_LAST_TIME();
        return dateTime.Date < DateTime.Now.Date;
    }

    bool KontrolGunlukAds()
    {
        DateTime dateTime = KAYIT.GetDAILY_CREDIT_REKLAM();
        return dateTime.Date < DateTime.Now.Date;
    }
    bool Kontrol15DakikalikAds()
    {

        DateTime dateTime = KAYIT.GetDAILY_CREDIT_REKLAM_15DK();
        return dateTime.TimeOfDay < DateTime.Now.TimeOfDay;

    }
    void GeriSayimlar()
    {

        if (!isReadyForDaily)
        {
            _txtDaily.text = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
            if (KontrolGunluk())
            {
                CheckTheDaily();
            }
        }

        if (countAds <= 0)
        {
            _txtAds.text = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
            if (KontrolGunlukAds())
            {
                CheckTheDailyAds();
            }
        }
        else if (countAds > 0 && !Kontrol15DakikalikAds()&&!isReadyForDailyAds)
        {
            DateTime dateTime = KAYIT.GetDAILY_CREDIT_REKLAM_15DK();
            _txtAds.text = (dateTime.TimeOfDay - DateTime.Now.TimeOfDay).ToString().Substring(0, 8);

            if (_txtAds.text=="00:00:00")
            {
                isReadyForDailyAds = true;
                CheckTheDailyAds();
            }
        }
        
    }

    private void HandleBack()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        AdControl.instance.CloseBanner();
        Destroy(gameObject);
    }
    public void ReklamComplete()
    {
        GameManagerVideoPoker.instance.AddCredits(50);
        KAYIT.SetDAILY_CREDIT_REKLAM_15DK(DateTime.Now.AddSeconds(15));
        countAds--;
        KAYIT.SetDAILY_CREDIT_REKLAM_COUNT(countAds);
        isReadyForDailyAds = false;
        _btnAds.interactable = false;
        CheckTheDailyAds();
    }
    private void HandleAds()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        AdControl.instance.ShowRewardedVideo();
    }
    private void HandleDaily()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        GameManagerVideoPoker.instance.AddCredits(50);
        KAYIT.SetDAILY_CREDIT_LAST_TIME(DateTime.Now);
        CheckTheDaily();
    }


}
