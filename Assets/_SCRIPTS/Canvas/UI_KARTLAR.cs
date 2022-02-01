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
        //_btnsKart[0].interactable = !(KART_SATIS.GetAktifKart() == 0);
        // _btnsKart[1].interactable = !(KART_SATIS.GetAktifKart() == 1);
        for (int i = 0; i < _btnsKart.Length; i++)
        {
            HandleAta(_btnsKart[i], i);
        }
    }

    void HandleAta(Button btn, int index)
    {
        int fiyat = GetPlayerPrefDeger(index);
        if (fiyat > 0)
        {
            btn.GetComponentInChildren<TMP_Text>().text = $"{fiyat}$";
            if (_anaPara >= fiyat)
            {
                btn.interactable = true;
                btn.onClick.AddListener(()=>HandleSatinAl(index,fiyat));
            }
        }
        else
        {
            btn.GetComponentInChildren<TMP_Text>().text = "USE";
            btn.interactable= !(KART_SATIS.GetAktifKart() == index);
            btn.onClick.AddListener(() => HandleKartKullan(index));
        }
    }

    int GetPlayerPrefDeger(int index)
    {
        switch (index)
        {
            case 0: return KART_SATIS.GetElli();
            case 1: return KART_SATIS.GetyetmisBes();
            case 2: return KART_SATIS.Getyuz();
            case 3: return KART_SATIS.GetyuzYirmiBes();
            case 4: return KART_SATIS.GetyuzElli();
            case 5: return KART_SATIS.GetyuzYetmisBes();
            case 6: return KART_SATIS.GetikiYuzMavi();
            case 7: return KART_SATIS.GetikiYuzKirmizi();
            case 8: return KART_SATIS.GetikiYuzElliMavi();
            case 9: return KART_SATIS.GetikiYuzElliKirmizi();
            case 10: return KART_SATIS.GetucYuzMavi();
            case 11: return KART_SATIS.GetucYuzKirmizi();
            case 12: return KART_SATIS.GetdortYuzMavi();
            case 13: return KART_SATIS.GetdortYuzKirmizi();
            case 14: return KART_SATIS.GetbesYuzMavi();
            case 15: return KART_SATIS.GetbesYuzKirmizi();
            case 16: return KART_SATIS.GetbaltiYuzMavi();
            case 17: return KART_SATIS.GetaltiYuzKirmizi();
            case 18: return KART_SATIS.GetyediYuzElliMavi();
            case 19: return KART_SATIS.GetyediYuzElliKirmizi();
            case 20: return KART_SATIS.GetbinMavi();
            case 21: return KART_SATIS.GetbinKirmizi();

            default:
                return 000000;

        }
    }

    void HandleKartKullan(int index)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);

        KART_SATIS.SetAktifKart(index);
        GameManagerVideoPoker.instance.AtaKartArkasi();
        AtaHandle();
    }
    void HandleSatinAl(int index,int fiyat)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KAYIT.AddToAnaBakiye(-fiyat);
        GuncelPara();
        SatinAl(index);
        AtaHandle();

    }
    void SatinAl(int index)
    {
        switch (index)
        {
            case 0:  KART_SATIS.SatildiElli(); break;
            case 1:  KART_SATIS.SatildiyuzYetmisBes(); break;
            case 2:  KART_SATIS.Satildiyuz(); break;
            case 3:  KART_SATIS.SatildiyuzYirmiBes(); break;
            case 4:  KART_SATIS.SatildiyuzElli(); break;
            case 5:  KART_SATIS.SatildiyuzYetmisBes(); break;
            case 6:  KART_SATIS.SatildiikiYuzMavi(); break;
            case 7:  KART_SATIS.SatildiikiYuzKirmizi(); break;
            case 8:  KART_SATIS.SatildiikiYuzElliMavi(); break;
            case 9:  KART_SATIS.SatildiikiYuzElliKirmizi(); break;
            case 10:  KART_SATIS.SatildiucYuzMavi(); break;
            case 11:  KART_SATIS.SatildiucYuzKirmizi(); break;
            case 12:  KART_SATIS.SatildidortYuzMavi(); break;
            case 13:  KART_SATIS.SatildidortYuzKirmizi(); break;
            case 14:  KART_SATIS.SatildibesYuzMavi(); break;
            case 15:  KART_SATIS.SatildibesYuzKirmizi(); break;
            case 16:  KART_SATIS.SatildibaltiYuzMavi(); break;
            case 17:  KART_SATIS.SatildialtiYuzKirmizi(); break;
            case 18:  KART_SATIS.SatildiyediYuzElliMavi(); break;
            case 19:  KART_SATIS.SatildiyediYuzElliKirmizi(); break;
            case 20:  KART_SATIS.SatildibinMavi(); break;
            case 21:  KART_SATIS.SatildibinKirmizi(); break;

            default:
                break;

        }
    }
    void SayfaDegistir(bool ileri)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        if (ileri)
        {
            _sayiSayfa++;
        }
        else
        {
            _sayiSayfa--;
        }
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
        switch (_sayiSayfa)
        {
            case 0: _btnPrev.interactable = false; break;
            case 4: _btnNext.interactable = false; break;
            default:
                _btnNext.interactable = true;
                _btnPrev.interactable = true;
                break;
        }
    }
    private void HandleClose()
    {
        AdControl.instance.CloseBanner();
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
