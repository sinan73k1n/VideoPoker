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
    [SerializeField] Text[] _txtGorevSayi;
    [SerializeField] Image[] _imgsGorevSayi,_sayfaNoktalar;
    [SerializeField] TMP_Text _txtGorevHeader;
    int _sayfaNumarasi;
    private void Awake()
    {
        _sayfaNumarasi = KAYIT.GetSayfaNumarasi_DAILY_AND_WEEK();
        ShowGorev(_sayfaNumarasi);
        SayfaDegistirNokta(_sayfaNumarasi);
    }

    void Start()
    {

        _btnClose.onClick.AddListener(() => HandleExit());
        _btnNext.onClick.AddListener(() => ChangePage(true));
        _btnPrev.onClick.AddListener(() => ChangePage(false));
        ChangeButtonActive(_sayfaNumarasi);
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
        ChangeButtonActive(_sayfaNumarasi);

        KAYIT.SetSayfaNumarasi_DAILY_AND_WEEK(_sayfaNumarasi);
        ShowGorev(_sayfaNumarasi);
        SayfaDegistirNokta(_sayfaNumarasi);


    }
    void ChangeButtonActive(int sayfa)
    {
        _btnNext.interactable = sayfa < 5;
        _btnPrev.interactable = sayfa > 0;
    }
    void HeaderDegistir(int sayfa)
    {
        switch (sayfa)
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
    void FillImage(Image image, int count, int max)
    {
        if (count != 0)
        {
            float deger = count / max;
            image.fillAmount = deger > 1 ? 1 : deger;
        }
        else
        {
            image.fillAmount = 0;
        }
    }
    void ShowGorev(int sayfa)
    {
        GOREV gorev0 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 0);
        GOREV gorev1 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 1);
        GOREV gorev2 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 2);

        _txtGorevName[0].text = gorev0._ad;
        _txtGorevName[1].text = gorev1._ad;
        _txtGorevName[2].text = gorev2._ad;


        _txtGorevSayi[0].text = "" + gorev0._tamamlanan + "/" + gorev0._tamamlanmasiGereken;
        _txtGorevSayi[1].text = "" + gorev1._tamamlanan + "/" + gorev1._tamamlanmasiGereken;
        _txtGorevSayi[2].text = "" + gorev2._tamamlanan + "/" + gorev2._tamamlanmasiGereken;
        _txtGorevSayi[3].text = "" + GetSayfaTamamlanma(gorev0.IsGorevTamam(),gorev1.IsGorevTamam(),gorev2.IsGorevTamam())+ "/3";

        FillImage(_imgsGorevSayi[0], gorev0._tamamlanan, gorev0._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[1], gorev0._tamamlanan, gorev1._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[2], gorev0._tamamlanan, gorev2._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[3], GetSayfaTamamlanma(gorev0.IsGorevTamam(), gorev1.IsGorevTamam(), gorev2.IsGorevTamam()), 3);

        _txtBtnName[0].text = $"{gorev0._odul * (sayfa != 5 ? (sayfa + 1) : 1)}$";
        _txtBtnName[1].text = $"{gorev1._odul * (sayfa != 5 ? (sayfa + 1) : 1)}$";
        _txtBtnName[2].text = $"{gorev2._odul * (sayfa != 5 ? (sayfa + 1) : 1)}$";
        int sonSayfa = sayfa == 5 ? 100 : (sayfa + 1) * 10;
        _txtBtnName[3].text = $"{sonSayfa}$";

        HeaderDegistir(sayfa);
    }

    int GetSayfaTamamlanma(params bool[] gorevler)
    {
        int a= 0;
        foreach (var item in gorevler)
        {
            if (item)
            {
                a++;
            }
        }
        return a;
    }
    void SayfaDegistirNokta(int sayfaNumarasi)
    {
        _sayfaNoktalar[0].enabled = sayfaNumarasi == 0;
        _sayfaNoktalar[1].enabled = sayfaNumarasi == 1;
        _sayfaNoktalar[2].enabled = sayfaNumarasi == 2;
        _sayfaNoktalar[3].enabled = sayfaNumarasi == 3;
        _sayfaNoktalar[4].enabled = sayfaNumarasi == 4;
        _sayfaNoktalar[5].enabled = sayfaNumarasi == 5;
    }
}