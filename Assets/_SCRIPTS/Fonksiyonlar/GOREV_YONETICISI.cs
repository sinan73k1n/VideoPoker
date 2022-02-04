using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GOREV_YONETICISI : MonoBehaviour
{
    public static GOREV_YONETICISI instance;

    public bool _isOpenTable = false;
    public string _sureKalanGun = "";
    public string _sureKalanHafta = "";
    string gun;

    string[] _GorevName = { "FOUR OF A KIND", "FULL HOUSE", "FLUSH", "STRAIGHT", "THREE OF A KIND", "TWO PAIR", "JACK OR BETTER", "GAME WIN", "GAME" };



    void Awake()
    {
        instance = this;
        Setup();
        //SetupDeneme();

    }
    private void Start()
    {
        //DEBUG_YAZDIR();

    }

    private void DEBUG_YAZDIR()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Debug.Log($"_GorevCount {i} / {j} : " + KAYIT_GOREV_YONETICISI.GetOneGorevCount(i, j));

            }
        }
        for (int j = 0; j < 6; j++)
        {
            Debug.Log($"_GorevCount {GorevList.win} {j} : " + KAYIT_GOREV_YONETICISI.GetOneGorevCount(7, j));
        }


        for (int i = 0; i < 3; i++)
        {

            Debug.Log($"_GorevBet 1 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(0, i));
            Debug.Log($"_GorevBet 2 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(1, i));
            Debug.Log($"_GorevBet 3 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(2, i));
            Debug.Log($"_GorevBet 4 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(3, i));
            Debug.Log($"_GorevBet 5 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(4, i));
            Debug.Log($"_GorevBet 6 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilen(5, i));

        }
        for (int i = 0; i < 3; i++)
        {

            Debug.Log($"Max 1 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(0, i));
            Debug.Log($"Max 2 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(1, i));
            Debug.Log($"Max 3 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(2, i));
            Debug.Log($"Max 4 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(3, i));
            Debug.Log($"Max 5 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(4, i));
            Debug.Log($"Max 6 {i}:" + KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(5, i));

        }
    }

    void Update()
    {

        GeriSayim();

    }

    void GeriSayim()
    {
        if (!_isOpenTable) return;
        _sureKalanGun = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
        _sureKalanHafta = gun + " Day " + ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);

    }
    public void Setup()
    {




        if (IsYeniHafta())
        {
            SifirlaSayimHaftalik();
            YeniGorevAta(0);
            YeniGorevAta(1);
            YeniGorevAta(2);
            YeniGorevAta(3);
            YeniGorevAta(4);
            YeniGorevAta(5);
            //SetupDeneme();

        }

        else if (IsYeniGun())
        {
            SifirlaSayimGunluk();
            YeniGorevAta(0);
            YeniGorevAta(1);
            YeniGorevAta(2);
            YeniGorevAta(3);
            YeniGorevAta(4);


        }


        int haftaGunu = Convert.ToInt32(DateTime.Now.DayOfWeek);
        gun = 7 == haftaGunu ? "0" : (7 - haftaGunu).ToString();
    }
    public void SetupDeneme()
    {


        SifirlaSayimGunluk();
        YeniGorevAta(0);
        YeniGorevAta(1);
        YeniGorevAta(2);
        YeniGorevAta(3);
        YeniGorevAta(4);

        SifirlaSayimHaftalik();
        YeniGorevAta(5);

        int haftaGunu = Convert.ToInt32(DateTime.Now.DayOfWeek);
        gun = 7 == haftaGunu ? "0" : (7 - haftaGunu).ToString();
    }




    void SifirlaSayimGunluk()
    {
        KAYIT_GOREV_YONETICISI.SifirlaGunluk(false);
    }
    void SifirlaSayimHaftalik()
    {
        KAYIT_GOREV_YONETICISI.SifirlaGunluk(true);
    }
    void YeniGorevAta(int hangi)
    {
        var _GorevNumaralari = GetNewList();

        for (var i = 0; i < 3; i++)
        {
            int secilen = _GorevNumaralari[UnityEngine.Random.Range(0, _GorevNumaralari.Count)];

            KAYIT_GOREV_YONETICISI.SetGorevSecilen(secilen, hangi, i);
            KAYIT_GOREV_YONETICISI.SetGorevSecilenMax(hangi != 5 ? GetMaxGunluk(secilen) : GetMaxHaftalik(secilen), hangi, i);
            _GorevNumaralari.Remove(secilen);
        }

    }
    public void YeniGorevAta(int hangiSayfa, int hangiSira, int hangiGorev)
    {

        KAYIT_GOREV_YONETICISI.SetGorevSecilen(hangiGorev, hangiSayfa, hangiSira);
        KAYIT_GOREV_YONETICISI.SetGorevSecilenMax(hangiSayfa != 5 ? GetMaxGunluk(hangiGorev) : GetMaxHaftalik(hangiGorev), hangiSayfa, hangiSira);



    }

    int GetMaxGunluk(int hangiGorev)
    {
        //return 2;
        switch (hangiGorev)
        {
            case 0:
            case 1:
            default: return UnityEngine.Random.Range(1, 3);
            case 2:
            case 3: return UnityEngine.Random.Range(1, 6);
            case 4: return UnityEngine.Random.Range(3, 9);
            case 5: return UnityEngine.Random.Range(5, 11);
            case 6: return UnityEngine.Random.Range(10, 31);
            case 7: return UnityEngine.Random.Range(20, 41);
            case 8: return UnityEngine.Random.Range(30, 51);

        }

    }
    int GetMaxHaftalik(int hangiGorev)
    {
        // return 2;
        switch (hangiGorev)
        {
            case 0:
            case 1:
            default: return UnityEngine.Random.Range(10, 16);
            case 2:
            case 3: return UnityEngine.Random.Range(15, 31);
            case 4: return UnityEngine.Random.Range(30, 51);
            case 5: return UnityEngine.Random.Range(40, 61);
            case 6: return UnityEngine.Random.Range(100, 301);
            case 7: return UnityEngine.Random.Range(200, 401);
            case 8: return UnityEngine.Random.Range(300, 501);


        }

    }

    List<int> GetNewList()
    {
        return new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    }




    bool IsYeniGun()
    {
        if (PlayerPrefs.GetString("YENIGUN") == string.Empty)
        {
            PlayerPrefs.SetString("YENIGUN", DateTime.Now.AddDays(1).ToString());
            return true;
        }
        else if (DateTime.Parse(PlayerPrefs.GetString("YENIGUN")).Date >= DateTime.Now.Date)
        {
            PlayerPrefs.SetString("YENIGUN", DateTime.Now.AddDays(1).ToString());
            return true;
        }
        return false;
    }

    bool IsYeniHafta()
    {

        if (PlayerPrefs.GetString("YENIHAFTA") == string.Empty)
        {
            int i = Convert.ToInt32(DateTime.Now.DayOfWeek);

            i = 7 - i;
            PlayerPrefs.SetString("YENIHAFTA", DateTime.Now.AddDays(i).ToString());
            return true;
        }
        else if (DateTime.Parse(PlayerPrefs.GetString("YENIHAFTA")).Date <= DateTime.Now.Date)
        {
            int i = Convert.ToInt32(DateTime.Now.DayOfWeek);
            i = 7 - i;
            PlayerPrefs.SetString("YENIHAFTA", DateTime.Now.AddDays(i).ToString());
            return true;
        }
        return false;




    }

    public GOREV GetGOREV(int sayfa, int kacinciGorev)
    {
        GOREV gorev = new GOREV();
        int tamamlanan = KAYIT_GOREV_YONETICISI.GetOneGorevCount(KAYIT_GOREV_YONETICISI.GetGorevSecilen(sayfa, kacinciGorev), sayfa);
        int tamamlanmasiGereken = KAYIT_GOREV_YONETICISI.GetGorevSecilenMax(sayfa, kacinciGorev);

        if (tamamlanan > tamamlanmasiGereken)
        {
            tamamlanan = tamamlanmasiGereken;
        }

        gorev._ad = _GorevName[KAYIT_GOREV_YONETICISI.GetGorevSecilen(sayfa, kacinciGorev)];
        gorev._tamamlanan = tamamlanan;
        gorev._tamamlanmasiGereken = tamamlanmasiGereken;
        gorev._odul = Convert.ToInt32(gorev._tamamlanmasiGereken * GetCarpan(KAYIT_GOREV_YONETICISI.GetGorevSecilen(sayfa, kacinciGorev)));
        gorev._odulAlindi = KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(sayfa, kacinciGorev);

        return gorev;

    }
    float GetCarpan(int gorevNumara)
    {
        switch (gorevNumara)
        {

            case 0: return 12.5f;
            case 1: return 4.5f;
            case 2: return 3f;
            case 3: return 2f;
            case 4: return 1.5f;
            case 5: return 1f;
            case 6: return .5f;
            case 7: return .2f;
            case 8:
            default: return .1f;

        }


    }

}



