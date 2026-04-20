using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KAYIT_GOREV_YONETICISI : MonoBehaviour
{
    const string GOREV = "gorev";
    const string SAYFA = "Sayfa";
    const string GOREV_SECILEN = "B";
    const string GOREV_MAX = "M";
    const string GOREV_TAMAMLANDI = "AS";

    static string GorevKey(int numara, int sayfa) => GOREV + numara + SAYFA + sayfa;

    public static void AddOneCountGorev(int numaraGorev, int Sayfa)
    {
        string key  = GorevKey(numaraGorev, Sayfa);
        string key2 = GorevKey(numaraGorev, 5);
        PlayerPrefs.SetInt(key,  PlayerPrefs.GetInt(key,  0) + 1);
        PlayerPrefs.SetInt(key2, PlayerPrefs.GetInt(key2, 0) + 1);
    }

    public static int GetOneGorevCount(int numaraGorev, int Sayfa)
    {
        return PlayerPrefs.GetInt(GorevKey(numaraGorev, Sayfa), 0);
    }

    public static void SifirlaGunluk(bool veHaftalik)
    {
        int limit = veHaftalik ? 6 : 5;

        for (int b = 0; b < 9; b++)
            for (int j = 0; j < limit; j++)
                PlayerPrefs.SetInt(GorevKey(b, j), 0);

        for (int e = 0; e < limit; e++)
            for (int q = 0; q < 4; q++)
                SetGorevTamamlandi(false, e, q);
    }

    public static void SetGorevSecilen(int gorev, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt(GOREV_SECILEN + hangiGorevBetOrWeek + GOREV_SECILEN + kacinciGorevSirasi, gorev); }
    public static int GetGorevSecilen(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt(GOREV_SECILEN + hangiGorevBetOrWeek + GOREV_SECILEN + kacinciGorevSirasi); }

    public static void SetGorevSecilenMax(int Max, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt(GOREV_MAX + hangiGorevBetOrWeek + GOREV_MAX + kacinciGorevSirasi, Max); }
    public static int GetGorevSecilenMax(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt(GOREV_MAX + hangiGorevBetOrWeek + GOREV_MAX + kacinciGorevSirasi); }

    public static void SetGorevTamamlandi(bool tamamlandi, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt(GOREV_TAMAMLANDI + hangiGorevBetOrWeek + GOREV_SECILEN + kacinciGorevSirasi, tamamlandi ? 1 : 0); }
    public static bool GetGorevTamamlandi(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt(GOREV_TAMAMLANDI + hangiGorevBetOrWeek + GOREV_SECILEN + kacinciGorevSirasi, 0) == 1; }



}
