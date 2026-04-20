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
        for (int i = 0; i < _btnsGorev.Length; i++)
        {
            int idx = i;
            _btnsGorev[i].onClick.AddListener(() => HandleGorev(idx));
        }
    }
    void HandleGorev(int hangi)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
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
