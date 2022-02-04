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
    [SerializeField] Image[] _imgsGorevSayi, _sayfaNoktalar;
    [SerializeField] TMP_Text _txtGorevHeader;
    int _sayfaNumarasi;
    bool _btn0 = false, _btn1 = false, _btn2 = false, _btn3 = false;
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
    private void Update()
    {
        YazBTN0AD();
        YazBTN1AD();
        YazBTN2AD();
        YazBTN3AD();
    }

    void YazBTN0AD()
    {
        if (!_btn0) return;
        if (_sayfaNumarasi != 5)
            _txtBtnName[0].text = GOREV_YONETICISI.instance._sureKalanGun;
        else
            _txtBtnName[0].text = GOREV_YONETICISI.instance._sureKalanHafta;

    }
    void YazBTN1AD()
    {
        if (!_btn1) return;
        if (_sayfaNumarasi != 5)
            _txtBtnName[1].text = GOREV_YONETICISI.instance._sureKalanGun;
        else
            _txtBtnName[1].text = GOREV_YONETICISI.instance._sureKalanHafta;

    }
    void YazBTN2AD()
    {
        if (!_btn2) return;
        if (_sayfaNumarasi != 5)
            _txtBtnName[2].text = GOREV_YONETICISI.instance._sureKalanGun;
        else
            _txtBtnName[2].text = GOREV_YONETICISI.instance._sureKalanHafta;

    }
    void YazBTN3AD()
    {
        if (!_btn3) return;
        if (_sayfaNumarasi != 5)
            _txtBtnName[3].text = GOREV_YONETICISI.instance._sureKalanGun;
        else
            _txtBtnName[3].text = GOREV_YONETICISI.instance._sureKalanHafta;

    }

    void HandleBos()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
    }

    void HandleExit()
    {
        GOREV_YONETICISI.instance._isOpenTable = false;
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
    void FillImage(Image image, float count, float max)
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

        int odul0 = gorev0._odul * (sayfa != 5 ? (sayfa + 1) : 1);
        int odul1 = gorev1._odul * (sayfa != 5 ? (sayfa + 1) : 1);
        int odul2 = gorev2._odul * (sayfa != 5 ? (sayfa + 1) : 1);
        int odul3 = sayfa == 5 ? 100 : (sayfa + 1) * 10;

        _btn0 = gorev0._odulAlindi;
        _btn1 = gorev1._odulAlindi;
        _btn2 = gorev2._odulAlindi;
        _btn3 = KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(sayfa, 3);

        _btnGorev[0].interactable = !_btn0 ? gorev0.IsGorevTamam() : !_btn0;
        _btnGorev[1].interactable = !_btn1 ? gorev1.IsGorevTamam() : !_btn1;
        _btnGorev[2].interactable = !_btn2 ? gorev2.IsGorevTamam() : !_btn2;
        _btnGorev[3].interactable = !_btn3 ? (gorev0.IsGorevTamam()&& gorev1.IsGorevTamam() && gorev2.IsGorevTamam()) : !_btn3;

        _txtGorevName[0].text = gorev0._ad;
        _txtGorevName[1].text = gorev1._ad;
        _txtGorevName[2].text = gorev2._ad;


        _txtGorevSayi[0].text = "" + gorev0._tamamlanan + "/" + gorev0._tamamlanmasiGereken;
        _txtGorevSayi[1].text = "" + gorev1._tamamlanan + "/" + gorev1._tamamlanmasiGereken;
        _txtGorevSayi[2].text = "" + gorev2._tamamlanan + "/" + gorev2._tamamlanmasiGereken;
        _txtGorevSayi[3].text = "" + GetSayfaTamamlanma(gorev0.IsGorevTamam(), gorev1.IsGorevTamam(), gorev2.IsGorevTamam()) + "/3";

        FillImage(_imgsGorevSayi[0], gorev0._tamamlanan, gorev0._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[1], gorev1._tamamlanan, gorev1._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[2], gorev2._tamamlanan, gorev2._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[3], GetSayfaTamamlanma(gorev0.IsGorevTamam(), gorev1.IsGorevTamam(), gorev2.IsGorevTamam()), 3);

        _txtBtnName[0].text = $"{odul0}$";
        _txtBtnName[1].text = $"{odul1}$";
        _txtBtnName[2].text = $"{odul2}$";
        _txtBtnName[3].text = $"{odul3}$";

        foreach (var item in _btnGorev)
        {
            item.onClick.RemoveAllListeners();
        }
        _btnGorev[0].onClick.AddListener(() => HandleOdulAl(sayfa, 0, odul0));
        _btnGorev[1].onClick.AddListener(() => HandleOdulAl(sayfa, 1, odul1));
        _btnGorev[2].onClick.AddListener(() => HandleOdulAl(sayfa, 2, odul2));
        _btnGorev[3].onClick.AddListener(() => HandleOdulAl(sayfa, 3, odul3));

        HeaderDegistir(sayfa);
    }

    void HandleOdulAl(int sayfa, int sira, int odul)
    {
        KAYIT_GOREV_YONETICISI.SetGorevTamamlandi(true, sayfa, sira);
        GameManagerVideoPoker.instance.AddCredits(odul);
        ShowGorev(_sayfaNumarasi);

    }

    int GetSayfaTamamlanma(params bool[] gorevler)
    {
        int a = 0;
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