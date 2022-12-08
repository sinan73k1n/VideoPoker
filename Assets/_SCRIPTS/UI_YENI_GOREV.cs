using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_YENI_GOREV : MonoBehaviour
{
    public static UI_YENI_GOREV instance;
    [SerializeField] Button[] _btnsGorev;
    [SerializeField] Button _btnsClose;
    [SerializeField] TMP_Text _txtHeader;
    [SerializeField] Sprite spriteMavi;

    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {
        _btnsClose.onClick.AddListener(HandleClose);
        _btnsGorev[0].onClick.AddListener(()=>HandleGorev(0));
        _btnsGorev[1].onClick.AddListener(()=>HandleGorev(1));
        _btnsGorev[2].onClick.AddListener(()=>HandleGorev(2));
        _btnsGorev[3].onClick.AddListener(()=>HandleGorev(3));
        _btnsGorev[4].onClick.AddListener(()=>HandleGorev(4));
        _btnsGorev[5].onClick.AddListener(()=>HandleGorev(5));
        _btnsGorev[6].onClick.AddListener(()=>HandleGorev(6));
        _btnsGorev[7].onClick.AddListener(()=>HandleGorev(7));
        _btnsGorev[8].onClick.AddListener(()=>HandleGorev(8));
    }
    void HandleGorev(int hangi)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        AdControl.instance.ShowRewardedVideoYeniGorev(hangi);
    }

    public void Basarili(bool basarili,int hangiGorev)
    {
        if (basarili)
        {
            foreach (var item in _btnsGorev)
            {
                item.interactable = false;
            }
            _btnsGorev[hangiGorev].image.sprite = spriteMavi;
            _btnsGorev[hangiGorev].onClick.RemoveAllListeners();
            _btnsGorev[hangiGorev].interactable = true;
            _txtHeader.text = "SUCCESSFUL!";

            GOREV_YONETICISI.instance.YeniGorevAta(CanvasDAILIY_AND_WEEKLY.instance._sayfaNumarasi, CanvasDAILIY_AND_WEEKLY.instance._btnSiraNumarasi, hangiGorev);
            CanvasDAILIY_AND_WEEKLY.instance.ShowGorev(CanvasDAILIY_AND_WEEKLY.instance._sayfaNumarasi);
        }
        else
        {
            _txtHeader.text = "FAILED!";
        }
    }
    private void HandleClose()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
}
