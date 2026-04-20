using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_KARTLAR : MonoBehaviour
{
    [SerializeField] GameObject[] _goSAYFALAR, _goKARTSAYFA;
    [SerializeField] Button _btnClose, _btnPrev, _btnNext;
    [SerializeField] TMP_Text _txtCredits;
    [SerializeField] Button[] _btnsKart;

    int _sayiSayfa = 0;
    int _anaPara;

    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleClose());
        _btnPrev.onClick.AddListener(() => SayfaDegistir(false));
        _btnNext.onClick.AddListener(() => SayfaDegistir(true));
        KontrolSayfa();
        SayfaDegistir(_sayiSayfa);
        GuncelPara();
        AtaHandle();
    }

    void AtaHandle()
    {
        foreach (var item in _btnsKart)
        {
            item.onClick.RemoveAllListeners();
            item.interactable = false;
        }
        for (int i = 0; i < _btnsKart.Length; i++)
            HandleAta(_btnsKart[i], i);
    }

    void HandleAta(Button btn, int index)
    {
        int fiyat = KART_SATIS.GetPrice(index);
        if (fiyat > 0)
        {
            btn.GetComponentInChildren<TMP_Text>().text = $"{fiyat}$";
            if (_anaPara >= fiyat)
            {
                btn.interactable = true;
                btn.onClick.AddListener(() => HandleSatinAl(index, fiyat));
            }
        }
        else
        {
            btn.GetComponentInChildren<TMP_Text>().text = "USE";
            btn.interactable = KART_SATIS.GetAktifKart() != index;
            btn.onClick.AddListener(() => HandleKartKullan(index));
        }
    }

    void HandleKartKullan(int index)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KART_SATIS.SetAktifKart(index);
        GameManagerVideoPoker.instance.AtaKartArkasi();
        AtaHandle();
    }

    void HandleSatinAl(int index, int fiyat)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KAYIT.AddToAnaBakiye(-fiyat);
        GuncelPara();
        KART_SATIS.SetSold(index);
        AtaHandle();
    }

    void SayfaDegistir(bool ileri)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _sayiSayfa += ileri ? 1 : -1;
        KontrolSayfa();
        SayfaDegistir(_sayiSayfa);
    }

    void GuncelPara()
    {
        _anaPara = KAYIT.GetAnaBakiye();
        _txtCredits.text = _anaPara.ToString();
    }

    void KontrolSayfa()
    {
        _btnPrev.interactable = _sayiSayfa > 0;
        _btnNext.interactable = _sayiSayfa < 4;
    }

    void HandleClose()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }

    void SayfaDegistir(int sayfa)
    {
        for (int i = 0; i < _goSAYFALAR.Length; i++)
        {
            bool deger = i == sayfa;
            _goSAYFALAR[i].SetActive(deger);
            _goKARTSAYFA[i].SetActive(deger);
        }
    }
}
