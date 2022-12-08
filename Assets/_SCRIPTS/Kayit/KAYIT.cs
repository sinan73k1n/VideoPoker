using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAYIT : MonoBehaviour
{

    const string ANA_BAKIYE = "AnaBakiye";
    const string SECILI_BAHIS = "SeciliBahis";

    const string OYNANAN_ROYAL_FLUSH = "OYNANAN_ROYAL_FLUSH";
    const string OYNANAN_STRAIGHT_FLUSH = "OYNANAN_STRAIGHT_FLUSH";
    const string OYNANAN_FOUR_A_KIND = "OYNANAN_FOUR_A_KIND";
    const string OYNANAN_FULL_HOUSE = "OYNANAN_FULL_HOUSE";
    const string OYNANAN_FLUSH = "OYNANAN_FLUSH";
    const string OYNANAN_STRAIGHT = "OYNANAN_STRAIGHT";
    const string OYNANAN_THREE_A_KIND = "OYNANAN_THREE_A_KIND";
    const string OYNANAN_TWO_PAIR = "OYNANAN_TWO_PAIR";
    const string OYNANAN_JACK_OR_BETTER = "OYNANAN_JACK_OR_BETTER";
    const string OYNANAN_GAME = "OYNANAN_GAME";
    const string OYNANAN_MAX_CREDIT = "OYNANAN_MAX_CREDIT";
    const string OYNANAN_WIN = "OYNANAN_WIN";
    const string SAYFA_NUMARASI_DAILY_AND_WEEK = "SAYFA_NUMARASI_DAILY_AND_WEEK";

    const string SON_EL_ROYAL_FLUSH = "SON_EL_ROYAL_FLUSH";
    const string SON_EL_STRAIGHT_FLUSH = "SON_EL_STRAIGHT_FLUSH";
    const string SON_EL_FOUR_A_KIND = "SON_EL_FOUR_A_KIND";
    const string SON_EL_FULL_HOUSE = "SON_EL_FULL_HOUSE";
    const string SON_EL_FLUSH = "SON_EL_FLUSH";
    const string SON_EL_STRAIGHT = "SON_EL_STRAIGHT";
    const string SON_EL_THREE_A_KIND = "SON_EL_THREE_A_KIND";
    const string SON_EL_TWO_PAIR = "SON_EL_TWO_PAIR";
    const string SON_EL_JACK_OR_BETTER = "SON_EL_JACK_OR_BETTER";

    const string DAILY_CREDIT = "DAILY_CREDIT";
    const string DAILY_CREDIT_REKLAM = "DAILY_CREDIT_REKLAM";
    const string DAILY_CREDIT_REKLAM_15DK = "DAILY_CREDIT_REKLAM_15DK";
    const string DAILY_CREDIT_REKLAM_COUNT = "DAILY_CREDIT_REKLAM_COUNT";

    public static bool GetDegerlendirmeAcildi() { return PlayerPrefs.GetInt("degerlendirme acildi", 0) == 0 ? false : true; }
    public static void SetDegerlendirmeAcildi(bool ac) {  PlayerPrefs.SetInt("degerlendirme acildi", ac? 1:0); }
    public static bool GetDegerlendirmeyiAc() { return PlayerPrefs.GetInt("degerlendirmeyiAc", 0) == 0 ? false : true; }
    public static void SetDegerlendirmeyiAc(bool ac) {  PlayerPrefs.SetInt("degerlendirmeyiAc",ac? 1:0); }
    public static void SetDegerlendirmeKalanSayi()
    {
        if (GetDegerlendirmeAcildi()) return;
        string key = "degerlendirmeye kalan adet";
        int i = PlayerPrefs.GetInt(key, 100);
        if (i == 0)
        {
            SetDegerlendirmeyiAc(true);
        }
        else
        {
            i--;
            PlayerPrefs.SetInt(key, i);
        }
    }

    public static bool GetReklamVar() { return PlayerPrefs.GetInt("ReklamVar",1) == 1 ? true : false; }
    public static void SetReklamVar(bool deger) { PlayerPrefs.SetInt("ReklamVar", deger ? 1 : 0); }


    public static void SetDAILY_CREDIT_LAST_TIME(DateTime dateTime) { PlayerPrefs.SetString(DAILY_CREDIT, dateTime.ToString()); }
    public static DateTime GetDAILY_CREDIT_LAST_TIME()
    {

        if (PlayerPrefs.GetString(DAILY_CREDIT) == string.Empty)
        {
            PlayerPrefs.SetString(DAILY_CREDIT, DateTime.Now.AddDays(-1).ToString());
        }
        return DateTime.Parse(PlayerPrefs.GetString(DAILY_CREDIT));
    }
    public static void SetDAILY_CREDIT_REKLAM(DateTime dateTime) { PlayerPrefs.SetString(DAILY_CREDIT_REKLAM, dateTime.ToString()); }
    public static DateTime GetDAILY_CREDIT_REKLAM()
    {

        if (PlayerPrefs.GetString(DAILY_CREDIT_REKLAM) == string.Empty)
        {
            PlayerPrefs.SetString(DAILY_CREDIT_REKLAM, DateTime.Now.AddDays(-1).ToString());
        }
        return DateTime.Parse(PlayerPrefs.GetString(DAILY_CREDIT_REKLAM));
    }
    public static void SetDAILY_CREDIT_REKLAM_15DK(DateTime dateTime) { PlayerPrefs.SetString(DAILY_CREDIT_REKLAM_15DK, dateTime.ToString()); }
    public static DateTime GetDAILY_CREDIT_REKLAM_15DK()
    {

        if (PlayerPrefs.GetString(DAILY_CREDIT_REKLAM_15DK) == string.Empty)
        {
            PlayerPrefs.SetString(DAILY_CREDIT_REKLAM_15DK, DateTime.Now.AddHours(-1).ToString());
        }
        return DateTime.Parse(PlayerPrefs.GetString(DAILY_CREDIT_REKLAM_15DK));
    }

    public static void SetDAILY_CREDIT_REKLAM_COUNT(int count) { PlayerPrefs.SetInt(DAILY_CREDIT_REKLAM_COUNT,count); }
    public static int GetDAILY_CREDIT_REKLAM_COUNT() {return PlayerPrefs.GetInt(DAILY_CREDIT_REKLAM_COUNT,5); }

    public static void SetSON_EL_ROYAL_FLUSH(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_ROYAL_FLUSH, kartlar);
    }
    public static List<string> GetSON_EL_ROYAL_FLUSH()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_ROYAL_FLUSH, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_STRAIGHT_FLUSH(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_STRAIGHT_FLUSH, kartlar);
    }
    public static List<string> GetSON_EL_STRAIGHT_FLUSH()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_STRAIGHT_FLUSH, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_FOUR_A_KIND(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_FOUR_A_KIND, kartlar);
    }
    public static List<string> GetSON_EL_FOUR_A_KIND()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_FOUR_A_KIND, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_FULL_HOUSE(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_FULL_HOUSE, kartlar);
    }
    public static List<string> GetSON_EL_FULL_HOUSE()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_FULL_HOUSE, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_FLUSH(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_FLUSH, kartlar);
    }
    public static List<string> GetSON_EL_FLUSH()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_FLUSH, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_STRAIGHT(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_STRAIGHT, kartlar);
    }
    public static List<string> GetSSON_EL_STRAIGHTH()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_STRAIGHT, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_THREE_A_KIND(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_THREE_A_KIND, kartlar);
    }
    public static List<string> GetSON_EL_THREE_A_KIND()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_THREE_A_KIND, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_TWO_PAIR(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_TWO_PAIR, kartlar);
    }
    public static List<string> GetSON_EL_TWO_PAIR()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_TWO_PAIR, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }
    public static void SetSON_EL_JACK_OR_BETTER(string kartlar)
    {
        PlayerPrefs.SetString(SON_EL_JACK_OR_BETTER, kartlar);
    }
    public static List<string> GetSON_EL_JACK_OR_BETTER()
    {
        List<string> list = new List<string>();
        string kartlar = PlayerPrefs.GetString(SON_EL_JACK_OR_BETTER, "00001111222233334444");
        list.Add(kartlar.Substring(0, 4));
        list.Add(kartlar.Substring(4, 4));
        list.Add(kartlar.Substring(8, 4));
        list.Add(kartlar.Substring(12, 4));
        list.Add(kartlar.Substring(16, 4));
        return list;
    }

    public static int GetAnaBakiye() { return PlayerPrefs.GetInt(ANA_BAKIYE, 50); }
    public static void AddToAnaBakiye(int neKadar) { PlayerPrefs.SetInt(ANA_BAKIYE, GetAnaBakiye() + neKadar); }

    public static int GetSeciliBahis() { return PlayerPrefs.GetInt(SECILI_BAHIS, 1); }
    public static void SetSeciliBahisiBirArttir()
    {
        int bahis = GetSeciliBahis();
        bahis++;
        if (bahis >= 6) { bahis = 1; }
        PlayerPrefs.SetInt(SECILI_BAHIS, bahis);

    }
    public static void SetSesSeviyesi(float seviye) { PlayerPrefs.SetFloat("Ses Seviye", seviye); }
    public static float GetSesSeviyesi() { return PlayerPrefs.GetFloat("Ses Seviye", 1f); }
    public static void AddOneHand(TypeOfPokerHand type)
    {

        switch (type)
        {
            case TypeOfPokerHand.RoyalFlush:
                PlayerPrefs.SetInt(OYNANAN_ROYAL_FLUSH, PlayerPrefs.GetInt(OYNANAN_ROYAL_FLUSH, 0) + 1);
                break;
            case TypeOfPokerHand.StraightFlush: PlayerPrefs.SetInt(OYNANAN_STRAIGHT_FLUSH, PlayerPrefs.GetInt(OYNANAN_STRAIGHT_FLUSH, 0) + 1); break;
            case TypeOfPokerHand.FourOfAKind:   PlayerPrefs.SetInt(OYNANAN_FOUR_A_KIND, PlayerPrefs.GetInt(OYNANAN_FOUR_A_KIND, 0) + 1); break;
            case TypeOfPokerHand.FullHouse:     PlayerPrefs.SetInt(OYNANAN_FULL_HOUSE, PlayerPrefs.GetInt(OYNANAN_FULL_HOUSE, 0) + 1); break;
            case TypeOfPokerHand.Flush:         PlayerPrefs.SetInt(OYNANAN_FLUSH, PlayerPrefs.GetInt(OYNANAN_FLUSH, 0) + 1); break;
            case TypeOfPokerHand.Straight:      PlayerPrefs.SetInt(OYNANAN_STRAIGHT, PlayerPrefs.GetInt(OYNANAN_STRAIGHT, 0) + 1); break;
            case TypeOfPokerHand.ThreeOfAKind:  PlayerPrefs.SetInt(OYNANAN_THREE_A_KIND, PlayerPrefs.GetInt(OYNANAN_THREE_A_KIND, 0) + 1); break;
            case TypeOfPokerHand.TwoPair:       PlayerPrefs.SetInt(OYNANAN_TWO_PAIR, PlayerPrefs.GetInt(OYNANAN_TWO_PAIR, 0) + 1); break;
            case TypeOfPokerHand.JackOrBetter:  PlayerPrefs.SetInt(OYNANAN_JACK_OR_BETTER, PlayerPrefs.GetInt(OYNANAN_JACK_OR_BETTER, 0) + 1); break;
            default:
                Debug.Log("001111");
                break;
        }

    }

    public static int GetCountOfHand(TypeOfPokerHand type)
    {
        switch (type)
        {

            case TypeOfPokerHand.RoyalFlush: return PlayerPrefs.GetInt(OYNANAN_ROYAL_FLUSH, 0);
            case TypeOfPokerHand.StraightFlush: return PlayerPrefs.GetInt(OYNANAN_STRAIGHT_FLUSH, 0);
            case TypeOfPokerHand.FourOfAKind: return PlayerPrefs.GetInt(OYNANAN_FOUR_A_KIND, 0);
            case TypeOfPokerHand.FullHouse: return PlayerPrefs.GetInt(OYNANAN_FULL_HOUSE, 0);
            case TypeOfPokerHand.Flush: return PlayerPrefs.GetInt(OYNANAN_FLUSH, 0);
            case TypeOfPokerHand.Straight: return PlayerPrefs.GetInt(OYNANAN_STRAIGHT, 0);
            case TypeOfPokerHand.ThreeOfAKind: return PlayerPrefs.GetInt(OYNANAN_THREE_A_KIND, 0);
            case TypeOfPokerHand.TwoPair: return PlayerPrefs.GetInt(OYNANAN_TWO_PAIR, 0);
            case TypeOfPokerHand.JackOrBetter: return PlayerPrefs.GetInt(OYNANAN_JACK_OR_BETTER, 0);
            default: return 01111;

        }

    }

    public static void AddOneMore_GAME() { PlayerPrefs.SetInt(OYNANAN_GAME, PlayerPrefs.GetInt(OYNANAN_GAME, 0) + 1); }
    public static int GetCountOf_GAME() { return PlayerPrefs.GetInt(OYNANAN_GAME, 0); }
    public static void AddOneMore_MAX_CREDIT() { PlayerPrefs.SetInt(OYNANAN_MAX_CREDIT, PlayerPrefs.GetInt(OYNANAN_MAX_CREDIT, 0) + 1); }
    public static int GetWinCountOf_MAX_CREDIT() { return PlayerPrefs.GetInt(OYNANAN_MAX_CREDIT, 0); }
    public static void AddOneMore_WIN() { PlayerPrefs.SetInt(OYNANAN_WIN, PlayerPrefs.GetInt(OYNANAN_WIN, 0) + 1); }
    public static int GetWinCountOf_WIN() { return PlayerPrefs.GetInt(OYNANAN_WIN, 0); }

    public static void SetSayfaNumarasi_DAILY_AND_WEEK(int sayfa) { PlayerPrefs.SetInt(SAYFA_NUMARASI_DAILY_AND_WEEK, sayfa); }
    public static int GetSayfaNumarasi_DAILY_AND_WEEK() { return PlayerPrefs.GetInt(SAYFA_NUMARASI_DAILY_AND_WEEK, 0); }



}