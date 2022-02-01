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
    int[,] _GorevCount = new int[8, 6];
    public enum gorevList { fourOfAKind, flush, straight, threeOfAKind, twoPair, jackOrBetter, win, game }
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
        _sureKalanGun = ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);
        _sureKalanHafta = gun + " Day " + ((DateTime.Parse("23:59:59") - DateTime.Now.TimeOfDay).TimeOfDay).ToString().Substring(0, 8);

    }
    public void Setup()
    {

        if (IsYeniGun())
        {
            //Yeni görevler
        }

        if (IsYeniHafta())
        {
            //Yeni görevler
        }

        int haftaGunu = Convert.ToInt32(DateTime.Now.DayOfWeek);
        gun = 7 == haftaGunu ? "0" : (7 - haftaGunu).ToString();
    }

    public void DoThis(gorevList gorev, int carpan)
    {
        switch (gorev)
        {
            case gorevList.fourOfAKind:
                FourAKindKazanmaSayisi(carpan);
                break;
            case gorevList.flush:
                FlushKazanmaSayisi(carpan);
                break;
            case gorevList.straight:
                StraightKazanmaSayisi(carpan);
                break;
            case gorevList.threeOfAKind:
                TreeAKindKazanmaSayisi(carpan);
                break;
            case gorevList.twoPair:
                TwoPairKazanmaSayisi(carpan);
                break;
            case gorevList.jackOrBetter:
                JackOrBetterKazanmaSayisi(carpan);
                break;
            case gorevList.win:
                OyunKazanmaSayisi(carpan);
                break;
            case gorevList.game:
                OyunSayisi(carpan);
                break;
            default:
                break;
        }

    }
    void FourAKindKazanmaSayisi(int carpan)
    {

        switch (carpan)
        {
            case 1: _GorevCount[0, 0]++; break;
            case 2: _GorevCount[0, 1]++; break;
            case 3: _GorevCount[0, 2]++; break;
            case 4: _GorevCount[0, 3]++; break;
            case 5: _GorevCount[0, 4]++; break;
            default:
                break;
        }
        _GorevCount[0, 5]++;


    }
    void FlushKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[1, 0]++; break;
            case 2: _GorevCount[1, 1]++; break;
            case 3: _GorevCount[1, 2]++; break;
            case 4: _GorevCount[1, 3]++; break;
            case 5: _GorevCount[1, 4]++; break;
            default:
                break;
        }
        _GorevCount[1, 5]++;
    }
    void StraightKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[2, 0]++; break;
            case 2: _GorevCount[2, 1]++; break;
            case 3: _GorevCount[2, 2]++; break;
            case 4: _GorevCount[2, 3]++; break;
            case 5: _GorevCount[2, 4]++; break;
            default:
                break;
        }
        _GorevCount[2, 5]++;
    }
    void TreeAKindKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[3, 0]++; break;
            case 2: _GorevCount[3, 1]++; break;
            case 3: _GorevCount[3, 2]++; break;
            case 4: _GorevCount[3, 3]++; break;
            case 5: _GorevCount[3, 4]++; break;
            default:
                break;
        }
        _GorevCount[3, 5]++;
    }
    void TwoPairKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[4, 0]++; break;
            case 2: _GorevCount[4, 1]++; break;
            case 3: _GorevCount[4, 2]++; break;
            case 4: _GorevCount[4, 3]++; break;
            case 5: _GorevCount[4, 4]++; break;
            default:
                break;
        }
        _GorevCount[4, 5]++;
    }
    void JackOrBetterKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[5, 0]++; break;
            case 2: _GorevCount[5, 1]++; break;
            case 3: _GorevCount[5, 2]++; break;
            case 4: _GorevCount[5, 3]++; break;
            case 5: _GorevCount[5, 4]++; break;
            default:
                break;
        }
        _GorevCount[5, 5]++;
    }
    void OyunKazanmaSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[6, 0]++; break;
            case 2: _GorevCount[6, 1]++; break;
            case 3: _GorevCount[6, 2]++; break;
            case 4: _GorevCount[6, 3]++; break;
            case 5: _GorevCount[6, 4]++; break;
            default:
                break;
        }
        _GorevCount[6, 5]++;
    }
    void OyunSayisi(int carpan)
    {
        switch (carpan)
        {
            case 1: _GorevCount[7, 0]++; break;
            case 2: _GorevCount[7, 1]++; break;
            case 3: _GorevCount[7, 2]++; break;
            case 4: _GorevCount[7, 3]++; break;
            case 5: _GorevCount[7, 4]++; break;
            default:
                break;
        }
        _GorevCount[7, 5]++;
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
            return false;
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

}
