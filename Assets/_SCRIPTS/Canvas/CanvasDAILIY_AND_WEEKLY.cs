using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CanvasDAILIY_AND_WEEKLY : MonoBehaviour
{
    [SerializeField] Button _btnClose, _btnPrev, _btnNext;
    [SerializeField] Button[] _btnGorev;

    [SerializeField] TMP_Text[] _txtGorevName, _txtBtnName;
    [SerializeField]Text[] _txtGorevSayi;
    [SerializeField]Image[] _imgsGorevSayi;
    [SerializeField] TMP_Text _txtGorevHeader;
    int _sayfaNumarasi;
    private void Awake()
    {
        _sayfaNumarasi = KAYIT.GetSayfaNumarasi_DAILY_AND_WEEK();

    }
 
    void Start()
    {
        
        _btnClose.onClick.AddListener(() => HandleExit());
        _btnNext.onClick.AddListener(() => ChangePage(true));
        _btnPrev.onClick.AddListener(() => ChangePage(false));
        foreach (var item in _btnGorev)
        {
            item.onClick.AddListener(HandleBos);
        }
    }

    void HandleBos()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
    }

    void HandleExit()
    {
        AdControl.instance.CloseBanner();
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }


    void ChangePage(bool ileri)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);

        if (ileri)
        {
            _sayfaNumarasi++;
            if (_sayfaNumarasi > 5) _sayfaNumarasi = 0;
        }
        else
        {
            _sayfaNumarasi--;
            if (_sayfaNumarasi < 0) _sayfaNumarasi = 5;
        }
        KAYIT.SetSayfaNumarasi_DAILY_AND_WEEK(_sayfaNumarasi);

        switch (_sayfaNumarasi)
        {
            case 0:
                _txtGorevHeader.text = "BET 1$";
                break;
            case 1:
                _txtGorevHeader.text = "BET 2$";
                break;
            case 2:
                _txtGorevHeader.text = "BET 3$";
                break;
            case 3:
                _txtGorevHeader.text = "BET 4$";
                break;
            case 4:
                _txtGorevHeader.text = "BET 5$";
                break;
            case 5:
                _txtGorevHeader.text = "WEEK";
                break;
            default:
                Debug.Log("Hata Sayfa Değiştirme");
                break;
        }

    }
}