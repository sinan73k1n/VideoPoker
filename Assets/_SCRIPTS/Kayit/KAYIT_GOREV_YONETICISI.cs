using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KAYIT_GOREV_YONETICISI : MonoBehaviour
{
   

    public static void AddOneCountGorev(int numaraGorev, int Sayfa)
    {
        string key = "gorev" + numaraGorev + "Sayfa" + Sayfa;
        string key2 = "gorev" + numaraGorev + "Sayfa" + 5;
        int i= PlayerPrefs.GetInt(key, 0)+1;
        int i2= PlayerPrefs.GetInt(key2, 0)+1;
        PlayerPrefs.SetInt(key, i);
        PlayerPrefs.SetInt(key2, i2);

    }
    public static int GetOneGorevCount(int numaraGorev, int Sayfa)
    {
        string key = "gorev" + numaraGorev + "Sayfa" + Sayfa;
        return PlayerPrefs.GetInt(key, 0);
    }
    public static void SifirlaGunluk(bool veHaftalik)
    {
        int i;
        int asa;
        if (veHaftalik)
        {
            i = 6;
            asa = 6;
        }
        else
        {
            i = 5;
            asa = 5;
        }

        for (int b = 0; b < 9; b++)
        {
            for (int j = 0; j < i; j++)
            {
                string key = "gorev" + b + "Sayfa" + j;
                PlayerPrefs.SetInt(key, 0);
            }
        }
        for (int e = 0; e < asa; e++)
        {
            for (int q = 0; q < 4; q++)
            {
                SetGorevTamamlandi(false, e, q);
            }
           
        }

    }

    public static void SetGorevSecilen(int gorev, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt("B" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi, gorev); }
    public static int GetGorevSecilen(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt("B" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi); }

    public static void SetGorevSecilenMax(int Max, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt("M" + hangiGorevBetOrWeek + "M" + kacinciGorevSirasi, Max); }
    public static int GetGorevSecilenMax(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt("M" + hangiGorevBetOrWeek + "M" + kacinciGorevSirasi); }

    public static void SetGorevTamamlandi(bool tamamlandi, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt("AS" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi, tamamlandi?1:0); }
    public static bool GetGorevTamamlandi(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt("AS" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi,0)==1; }



}
