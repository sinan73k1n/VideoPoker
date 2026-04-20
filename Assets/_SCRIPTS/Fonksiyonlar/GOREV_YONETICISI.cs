using System;
using System.Collections.Generic;
using UnityEngine;

public class GOREV_YONETICISI : MonoBehaviour
{
    public static GOREV_YONETICISI instance;

    public bool   _isOpenTable  = false;
    public string _sureKalanGun  = "";
    public string _sureKalanHafta = "";
    string _gun;

    const string YENI_GUN   = "YENIGUN";
    const string YENI_HAFTA = "YENIHAFTA";

    static readonly string[] _GorevName   = { "FOUR OF A KIND", "FULL HOUSE", "FLUSH", "STRAIGHT", "THREE OF A KIND", "TWO PAIR", "JACKS OR BETTER", "GAME WIN", "GAME" };
    static readonly float[]  _GorevCarpan = { 12.5f, 4.5f, 3f, 2f, 1.5f, 1f, 0.5f, 0.2f, 0.1f };

    void Awake()
    {
        instance = this;
        Setup();
    }

    void Update()
    {
        GeriSayim();
    }

    void GeriSayim()
    {
        if (!_isOpenTable) return;
        string kalan    = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
        _sureKalanGun   = kalan;
        _sureKalanHafta = _gun + " Day " + kalan;
    }

    public void Setup()
    {
        if (IsYeniHafta())
        {
            KAYIT_GOREV_YONETICISI.SifirlaGunluk(true);
            for (int i = 0; i < 6; i++) YeniGorevAta(i);
        }
        else if (IsYeniGun())
        {
            KAYIT_GOREV_YONETICISI.SifirlaGunluk(false);
            for (int i = 0; i < 5; i++) YeniGorevAta(i);
        }

        int haftaGunu = Convert.ToInt32(DateTime.Now.DayOfWeek);
        _gun = haftaGunu == 0 ? "0" : (7 - haftaGunu).ToString();
    }

    void YeniGorevAta(int hangi)
    {
        var gorevNumaralari = GetNewList();
        for (int i = 0; i < 3; i++)
        {
            int secilen = gorevNumaralari[UnityEngine.Random.Range(0, gorevNumaralari.Count)];
            KAYIT_GOREV_YONETICISI.SetGorevSecilen(secilen, hangi, i);
            KAYIT_GOREV_YONETICISI.SetGorevSecilenMax(hangi != 5 ? GetMaxGunluk(secilen) : GetMaxHaftalik(secilen), hangi, i);
            gorevNumaralari.Remove(secilen);
        }
    }

    public void YeniGorevAta(int hangiSayfa, int hangiSira, int hangiGorev)
    {
        KAYIT_GOREV_YONETICISI.SetGorevSecilen(hangiGorev, hangiSayfa, hangiSira);
        KAYIT_GOREV_YONETICISI.SetGorevSecilenMax(hangiSayfa != 5 ? GetMaxGunluk(hangiGorev) : GetMaxHaftalik(hangiGorev), hangiSayfa, hangiSira);
    }

    int GetMaxGunluk(int hangiGorev)
    {
        switch (hangiGorev)
        {
            case 0:
            case 1:
            default: return UnityEngine.Random.Range(1,  3);
            case 2:
            case 3:  return UnityEngine.Random.Range(1,  6);
            case 4:  return UnityEngine.Random.Range(3,  9);
            case 5:  return UnityEngine.Random.Range(5,  11);
            case 6:  return UnityEngine.Random.Range(10, 31);
            case 7:  return UnityEngine.Random.Range(20, 41);
            case 8:  return UnityEngine.Random.Range(30, 51);
        }
    }

    int GetMaxHaftalik(int hangiGorev)
    {
        switch (hangiGorev)
        {
            case 0:
            case 1:
            default: return UnityEngine.Random.Range(10,  16);
            case 2:
            case 3:  return UnityEngine.Random.Range(15,  31);
            case 4:  return UnityEngine.Random.Range(30,  51);
            case 5:  return UnityEngine.Random.Range(40,  61);
            case 6:  return UnityEngine.Random.Range(100, 301);
            case 7:  return UnityEngine.Random.Range(200, 401);
            case 8:  return UnityEngine.Random.Range(300, 501);
        }
    }

    List<int> GetNewList() => new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    bool IsYeniGun()
    {
        if (PlayerPrefs.GetString(YENI_GUN) == string.Empty)
        {
            PlayerPrefs.SetString(YENI_GUN, DateTime.Now.AddDays(1).ToString());
            return true;
        }
        if (DateTime.Parse(PlayerPrefs.GetString(YENI_GUN)).Date <= DateTime.Now.Date)
        {
            PlayerPrefs.SetString(YENI_GUN, DateTime.Now.AddDays(1).ToString());
            return true;
        }
        return false;
    }

    bool IsYeniHafta()
    {
        if (PlayerPrefs.GetString(YENI_HAFTA) == string.Empty)
        {
            int gun = Convert.ToInt32(DateTime.Now.DayOfWeek);
            PlayerPrefs.SetString(YENI_HAFTA, DateTime.Now.AddDays(7 - gun).ToString());
            return true;
        }
        if (DateTime.Parse(PlayerPrefs.GetString(YENI_HAFTA)).Date < DateTime.Now.Date)
        {
            int gun = Convert.ToInt32(DateTime.Now.DayOfWeek);
            PlayerPrefs.SetString(YENI_HAFTA, DateTime.Now.AddDays(7 - gun).ToString());
            return true;
        }
        return false;
    }

    public GOREV GetGOREV(int sayfa, int kacinciGorev)
    {
        int gorevNo    = KAYIT_GOREV_YONETICISI.GetGorevSecilen(sayfa, kacinciGorev);
        int tamamlanan = KAYIT_GOREV_YONETICISI.GetOneGorevCount(gorevNo, sayfa);
        int hedef      = KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(sayfa, kacinciGorev);

        return new GOREV
        {
            _ad                  = _GorevName[gorevNo],
            _tamamlanan          = tamamlanan > hedef ? hedef : tamamlanan,
            _tamamlanmasiGereken = hedef,
            _odul                = Convert.ToInt32(hedef * _GorevCarpan[gorevNo]),
            _odulAlindi          = KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(sayfa, kacinciGorev)
        };
    }
}
