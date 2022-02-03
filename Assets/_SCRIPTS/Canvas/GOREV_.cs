using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GOREV_ : MonoBehaviour
{
    public enum _gorev { Bet1, Bet2, Bet3, Bet4, Bet5, Hafta }


   
    public int index;
    public string Ad;
    public int Toplam;
    public int Toplanan;
    public bool GorevTamam() { return (Toplam == Toplanan); }

    

    public class GorevFourOfAKind
    {
        public const int minimum = 1;
        public const int maxsimum = 3;
        public const int minimumHafta = 10;
        public const int maxsimumHafta = 15;
        public const float costForOne = 12.5f;
    }
    public class GorevFullHouse
    {
        public const int minimum = 1;
        public const int maxsimum = 5;
        public const int minimumHafta = 10;
        public const int maxsimumHafta = 15;
        public const float costForOne = 4.5f;

    }
    public class GorevFlush
    {
        public const int minimum = 1;
        public const int maxsimum = 5;
        public const int minimumHafta = 15;
        public const int maxsimumHafta = 30;
        public const float costForOne = 3f;

    }
    public class GorevStraight
    {
        public const int minimum = 1;
        public const int maxsimum = 5;
        public const int minimumHafta = 15;
        public const int maxsimumHafta = 30;
        public const float costForOne = 4f;

    }
    public class GorevThereeOfAKind
    {
        public const int minimum = 3;
        public const int maxsimum = 8;
        public const int minimumHafta = 30;
        public const int maxsimumHafta = 50;
        public const float costForOne = 1.5f;
    }
    public class GorevTwoPair
    {
        public const int minimum = 5;
        public const int maxsimum = 10;
        public const int minimumHafta = 40;
        public const int maxsimumHafta = 60;
        public const float costForOne = 1f;

    }
    public class GorevJackOrBetter
    {
        public const int minimum = 30;
        public const int maxsimum = 50;
        public const int minimumHafta = 100;
        public const int maxsimumHafta = 300;
        public const float costForOne = 0.5f;

    }
    public class GorevGame
    {
        public const int minimum = 100;
        public const int maxsimum = 200;
        public const int minimumHafta = 500;
        public const int maxsimumHafta = 700;
        public const float costForOne = 12.5f;

    }
    public class GorevWin
    {
        public const int minimum = 30;
        public const int maxsimum = 50;
        public const int minimumHafta = 200;
        public const int maxsimumHafta = 500;
        public const float costForOne = 0.5f;

    }



}