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
        int i2= PlayerPrefs.GetInt(key, 0)+1;
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
        if (veHaftalik)
        {
            i = 9;
        }
        else
        {
            i = 8;
        }

        for (int b = 0; b < i; b++)
        {
            for (int j = 0; j < 6; j++)
            {
                string key = "gorev" + b + "Sayfa" + j;
                PlayerPrefs.SetInt(key, 0);
            }
        }
    }

    public static void SetGorevSecilen(int gorev, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt("B" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi, gorev); }
    public static int GetGorevSecilen(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt("B" + hangiGorevBetOrWeek + "B" + kacinciGorevSirasi); }

    public static void SetGorevSecilenMax(int Max, int hangiGorevBetOrWeek, int kacinciGorevSirasi) { PlayerPrefs.SetInt("M" + hangiGorevBetOrWeek + "M" + kacinciGorevSirasi, Max); }
    public static int GetGorevSecilenMax(int hangiGorevBetOrWeek, int kacinciGorevSirasi) { return PlayerPrefs.GetInt("M" + hangiGorevBetOrWeek + "M" + kacinciGorevSirasi); }



}
