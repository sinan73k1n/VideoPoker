using System;
using UnityEngine;

// Oyunla yan modüller (başarı, sıralama...) arasındaki TEK bağ.
// Oyun yalnız "şu oldu" der; kimin dinlediğini bilmez. Dinleyen yoksa hiçbir şey olmaz,
// dinleyen hata atarsa oyun etkilenmez (her dinleyici ayrı try/catch).
public static class OYUN_OLAYLARI
{
    public static event Action<int>       ElBasladi;        // konan bahis
    public static event Action<EL_SONUCU> ElBitti;
    public static event Action            GorevOdulAlindi;
    public static event Action            KartArkasiAcildi;

    public static void ElBasladiBildir(int bahis)      => Guvenli(ElBasladi, d => ((Action<int>)d)(bahis));
    public static void ElBittiBildir(EL_SONUCU sonuc)  => Guvenli(ElBitti, d => ((Action<EL_SONUCU>)d)(sonuc));
    public static void GorevOdulAlindiBildir()         => Guvenli(GorevOdulAlindi, d => ((Action)d)());
    public static void KartArkasiAcildiBildir()        => Guvenli(KartArkasiAcildi, d => ((Action)d)());

    static void Guvenli(Delegate olay, Action<Delegate> cagir)
    {
        if (olay == null) return;
        foreach (var dinleyici in olay.GetInvocationList())
        {
            try { cagir(dinleyici); }
            catch (Exception e) { Debug.LogException(e); }
        }
    }
}

public struct EL_SONUCU
{
    public int    carpan;            // PokerHandEvaluator.Evaluate dönüşü (0 = kayıp)
    public int    bahis;
    public int    kazanc;            // ekrana yazılan WIN (Royal max bahiste 4000)
    public string kartlar;           // PokerHandEvaluator.GetEl biçimi: "KU01MA01..."
    public int    ilkDagitimCarpan;  // DEAL anındaki el (hazır gelen Royal için)

    public TypeOfPokerHand? El
    {
        get
        {
            switch (carpan)
            {
                case 250: return TypeOfPokerHand.RoyalFlush;
                case 50:  return TypeOfPokerHand.StraightFlush;
                case 25:  return TypeOfPokerHand.FourOfAKind;
                case 9:   return TypeOfPokerHand.FullHouse;
                case 6:   return TypeOfPokerHand.Flush;
                case 4:   return TypeOfPokerHand.Straight;
                case 3:   return TypeOfPokerHand.ThreeOfAKind;
                case 2:   return TypeOfPokerHand.TwoPair;
                case 1:   return TypeOfPokerHand.JackOrBetter;
                default:  return null;
            }
        }
    }
}
