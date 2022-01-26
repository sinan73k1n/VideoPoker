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

    public static void AddOneMore_ROYAL_FLUSH() { PlayerPrefs.SetInt(OYNANAN_ROYAL_FLUSH, PlayerPrefs.GetInt(OYNANAN_ROYAL_FLUSH, 0) + 1); }
    public static int GetWinCountOf_ROYAL_FLUSH() { return PlayerPrefs.GetInt(OYNANAN_ROYAL_FLUSH, 0); }
    public static void AddOneMore_STRAIGHT_FLUSH() { PlayerPrefs.SetInt(OYNANAN_STRAIGHT_FLUSH, PlayerPrefs.GetInt(OYNANAN_STRAIGHT_FLUSH, 0) + 1); }
    public static int GetWinCountOf_STRAIGHT_FLUSH() { return PlayerPrefs.GetInt(OYNANAN_STRAIGHT_FLUSH, 0); }
    public static void AddOneMore_FOUR_A_KIND() { PlayerPrefs.SetInt(OYNANAN_FOUR_A_KIND, PlayerPrefs.GetInt(OYNANAN_FOUR_A_KIND, 0) + 1); }
    public static int GetWinCountOf_FOUR_A_KIND() { return PlayerPrefs.GetInt(OYNANAN_FOUR_A_KIND, 0); }
    public static void AddOneMore_FULL_HOUSE() { PlayerPrefs.SetInt(OYNANAN_FULL_HOUSE, PlayerPrefs.GetInt(OYNANAN_FULL_HOUSE, 0) + 1); }
    public static int GetWinCountOf_FULL_HOUSE() { return PlayerPrefs.GetInt(OYNANAN_FULL_HOUSE, 0); }
    public static void AddOneMore_FLUSH() { PlayerPrefs.SetInt(OYNANAN_FLUSH, PlayerPrefs.GetInt(OYNANAN_FLUSH, 0) + 1); }
    public static int GetWinCountOf_FLUSH() { return PlayerPrefs.GetInt(OYNANAN_FLUSH, 0); }
    public static void AddOneMore_STRAIGHT() { PlayerPrefs.SetInt(OYNANAN_STRAIGHT, PlayerPrefs.GetInt(OYNANAN_STRAIGHT, 0) + 1); }
    public static int GetWinCountOf_STRAIGHT() { return PlayerPrefs.GetInt(OYNANAN_STRAIGHT, 0); }
    public static void AddOneMore_THREE_A_KIND() { PlayerPrefs.SetInt(OYNANAN_THREE_A_KIND, PlayerPrefs.GetInt(OYNANAN_THREE_A_KIND, 0) + 1); }
    public static int GetWinCountOf_THREE_A_KIND() { return PlayerPrefs.GetInt(OYNANAN_THREE_A_KIND, 0); }
    public static void AddOneMore_TWO_PAIR() { PlayerPrefs.SetInt(OYNANAN_TWO_PAIR, PlayerPrefs.GetInt(OYNANAN_TWO_PAIR, 0) + 1); }
    public static int GetWinCountOf_TWO_PAIR() { return PlayerPrefs.GetInt(OYNANAN_TWO_PAIR, 0); }
    public static void AddOneMore_JACK_OR_BETTER() { PlayerPrefs.SetInt(OYNANAN_JACK_OR_BETTER, PlayerPrefs.GetInt(OYNANAN_JACK_OR_BETTER, 0) + 1); }
    public static int GetWinCountOf_JACK_OR_BETTER() { return PlayerPrefs.GetInt(OYNANAN_JACK_OR_BETTER, 0); }
    public static void AddOneMore_GAME() { PlayerPrefs.SetInt(OYNANAN_GAME, PlayerPrefs.GetInt(OYNANAN_GAME, 0) + 1); }
    public static int GetWinCountOf_GAME() { return PlayerPrefs.GetInt(OYNANAN_GAME, 0); }
    public static void AddOneMore_MAX_CREDIT() { PlayerPrefs.SetInt(OYNANAN_MAX_CREDIT, PlayerPrefs.GetInt(OYNANAN_MAX_CREDIT, 0) + 1); }
    public static int GetWinCountOf_MAX_CREDIT() { return PlayerPrefs.GetInt(OYNANAN_MAX_CREDIT, 0); }
    public static void AddOneMore_WIN() { PlayerPrefs.SetInt(OYNANAN_WIN, PlayerPrefs.GetInt(OYNANAN_WIN, 0) + 1); }
    public static int GetWinCountOf_WIN() { return PlayerPrefs.GetInt(OYNANAN_WIN, 0); }

    public static void SetSayfaNumarasi_DAILY_AND_WEEK(int sayfa) { PlayerPrefs.SetInt(SAYFA_NUMARASI_DAILY_AND_WEEK, sayfa); }
    public static int GetSayfaNumarasi_DAILY_AND_WEEK() {return PlayerPrefs.GetInt(SAYFA_NUMARASI_DAILY_AND_WEEK, 0); }



}
