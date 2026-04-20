using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CanvasDAILIY_AND_WEEKLY : MonoBehaviour
{
    public static CanvasDAILIY_AND_WEEKLY instance;
    [SerializeField] Button _btnClose, _btnPrev, _btnNext;
    [SerializeField] Button[] _btnGorev;

    [SerializeField] TMP_Text[] _txtGorevName, _txtBtnName;
    [SerializeField] Text[]     _txtGorevSayi;
    [SerializeField] Image[]    _imgsGorevSayi, _sayfaNoktalar;
    [SerializeField] TMP_Text   _txtGorevHeader;

    public int _sayfaNumarasi;
    public int _btnSiraNumarasi = 0;

    bool[] _odulAlindi = new bool[4];

    void Awake()
    {
        instance = this;
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
    }

    void Update()
    {
        if (!GOREV_YONETICISI.instance._isOpenTable) return;
        string sure = _sayfaNumarasi != 5
            ? GOREV_YONETICISI.instance._sureKalanGun
            : GOREV_YONETICISI.instance._sureKalanHafta;
        for (int i = 0; i < 4; i++)
        {
            if (_odulAlindi[i]) _txtBtnName[i].text = sure;
        }
    }

    void HandleExit()
    {
        GOREV_YONETICISI.instance._isOpenTable = false;
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }

    void ChangePage(bool ileri)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _sayfaNumarasi += ileri ? 1 : -1;
        if (_sayfaNumarasi > 5) _sayfaNumarasi = 0;
        if (_sayfaNumarasi < 0) _sayfaNumarasi = 5;
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
        _txtGorevHeader.text = sayfa < 5 ? $"BET {sayfa + 1}$" : "WEEKLY";
    }

    void FillImage(Image image, float count, float max)
    {
        float deger = count != 0 ? count / max : 0;
        image.fillAmount = deger > 1 ? 1 : deger;
    }

    public void ShowGorev(int sayfa)
    {
        GOREV gorev0 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 0);
        GOREV gorev1 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 1);
        GOREV gorev2 = GOREV_YONETICISI.instance.GetGOREV(sayfa, 2);

        int carpan = sayfa != 5 ? (sayfa + 1) : 1;
        int odul0  = gorev0._odul * carpan;
        int odul1  = gorev1._odul * carpan;
        int odul2  = gorev2._odul * carpan;
        int odul3  = sayfa == 5 ? 100 : (sayfa + 1) * 10;

        _odulAlindi[0] = gorev0._odulAlindi;
        _odulAlindi[1] = gorev1._odulAlindi;
        _odulAlindi[2] = gorev2._odulAlindi;
        _odulAlindi[3] = KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(sayfa, 3);

        bool hepsiTamam = gorev0.IsGorevTamam() && gorev1.IsGorevTamam() && gorev2.IsGorevTamam();
        _btnGorev[0].interactable = !_odulAlindi[0] && gorev0.IsGorevTamam();
        _btnGorev[1].interactable = !_odulAlindi[1] && gorev1.IsGorevTamam();
        _btnGorev[2].interactable = !_odulAlindi[2] && gorev2.IsGorevTamam();
        _btnGorev[3].interactable = !_odulAlindi[3] && hepsiTamam;

        _txtGorevName[0].text = gorev0._ad;
        _txtGorevName[1].text = gorev1._ad;
        _txtGorevName[2].text = gorev2._ad;

        _txtGorevSayi[0].text = $"{gorev0._tamamlanan}/{gorev0._tamamlanmasiGereken}";
        _txtGorevSayi[1].text = $"{gorev1._tamamlanan}/{gorev1._tamamlanmasiGereken}";
        _txtGorevSayi[2].text = $"{gorev2._tamamlanan}/{gorev2._tamamlanmasiGereken}";
        _txtGorevSayi[3].text = $"{GetSayfaTamamlanma(gorev0.IsGorevTamam(), gorev1.IsGorevTamam(), gorev2.IsGorevTamam())}/3";

        FillImage(_imgsGorevSayi[0], gorev0._tamamlanan, gorev0._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[1], gorev1._tamamlanan, gorev1._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[2], gorev2._tamamlanan, gorev2._tamamlanmasiGereken);
        FillImage(_imgsGorevSayi[3], GetSayfaTamamlanma(gorev0.IsGorevTamam(), gorev1.IsGorevTamam(), gorev2.IsGorevTamam()), 3);

        _txtBtnName[0].text = $"{odul0}$";
        _txtBtnName[1].text = $"{odul1}$";
        _txtBtnName[2].text = $"{odul2}$";
        _txtBtnName[3].text = $"{odul3}$";

        foreach (var item in _btnGorev) item.onClick.RemoveAllListeners();
        _btnGorev[0].onClick.AddListener(() => HandleOdulAl(sayfa, 0, odul0));
        _btnGorev[1].onClick.AddListener(() => HandleOdulAl(sayfa, 1, odul1));
        _btnGorev[2].onClick.AddListener(() => HandleOdulAl(sayfa, 2, odul2));
        _btnGorev[3].onClick.AddListener(() => HandleOdulAl(sayfa, 3, odul3));

        HeaderDegistir(sayfa);
    }

    void HandleOdulAl(int sayfa, int sira, int odul)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KAYIT_GOREV_YONETICISI.SetGorevTamamlandi(true, sayfa, sira);
        GameManagerVideoPoker.instance.AddCredits(odul);
        ShowGorev(_sayfaNumarasi);
        GameManagerVideoPoker.instance.ShowRewardCount();
    }

    int GetSayfaTamamlanma(params bool[] gorevler)
    {
        int a = 0;
        foreach (var item in gorevler) if (item) a++;
        return a;
    }

    void SayfaDegistirNokta(int sayfaNumarasi)
    {
        for (int i = 0; i < _sayfaNoktalar.Length; i++)
            _sayfaNoktalar[i].enabled = i == sayfaNumarasi;
    }
}
