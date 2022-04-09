using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasMENU : MonoBehaviour
{
    [SerializeField] Button _btnHow, _btnIstatistic, _btnAyar, _btnBack, _btnExit,_btnAds;
    [SerializeField] GameObject _goHow, _goIstatistic, _goAyar;
    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 10;
        
    }
    void Start()
    {

        AtaHandlesToButtons();

    }
    void AtaHandlesToButtons()
    {
        _btnHow.onClick.AddListener(() => HandleOpen(_goHow));
        _btnIstatistic.onClick.AddListener(() => HandleOpen(_goIstatistic));
        _btnAyar.onClick.AddListener(() => HandleOpen(_goAyar));
        _btnBack.onClick.AddListener(HandleBack);
        _btnExit.onClick.AddListener(() => StartCoroutine(HandleExit()));
        if (KAYIT.GetReklamVar())
        {
            _btnAds.onClick.AddListener(HandleAds);
        }
        else
        {
            _btnAds.gameObject.SetActive(false);
        }
    }

   void HandleAds()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
    }
    void HandleOpen(GameObject hangi)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Instantiate(hangi);
    }

    void HandleBack()
    {
        AdControl.instance.CloseBanner();
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        gameObject.SetActive(false);
    }
    IEnumerator HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void SetActiveAdsButton(bool deger) { _btnAds.gameObject.SetActive(deger); }
}