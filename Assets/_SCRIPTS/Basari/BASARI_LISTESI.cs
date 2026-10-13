using System;
using System.Collections.Generic;
using UnityEngine;

// ⛔ Enum'lar asset'e SAYI olarak yazılır: yeni değer yalnız SONA eklenir, araya girilmez, silinmez.
public enum BASARI_SAYAC
{
    OynananEl, RoyalFlush, StraightFlush, FourOfAKind, FullHouse, Flush, Straight, ThreeOfAKind, TwoPair, JacksOrBetter,
    ToplamKazanc, KazancSerisi, GorevOdulu, KartArkasi,
    OzelMaxBahisRoyal, OzelIlkDagitimRoyal, OzelDortAs
}

public enum BASARI_KADEME { Bronz, Gumus, Altin, Elmas, Gizli }

[Serializable]
public class BASARI_TANIM
{
    // ⛔ id yayından sonra DEĞİŞMEZ — kayıt anahtarı ondan üretilir (KAYIT kuralı §1).
    public string        id;
    public string        ad;
    public string        aciklama;
    public Sprite        ikon;
    public BASARI_SAYAC  sayac;
    public int           hedef = 1;
    public BASARI_KADEME kademe;
    public int           puan = 5;      // Play Games: 5'in katı, ≤200, toplam ≤2000
    public bool          gizli;
    public string        androidId;     // Play Console'daki başarı kimliği (CgkI...)
    public string        iosId;         // App Store Connect kimliği (ileride)
}

// Android ve iOS için TEK liste; platform kimlikleri her satırda.
[CreateAssetMenu(menuName = "Video Poker/Basari Listesi")]
public class BASARI_LISTESI : ScriptableObject
{
    public List<BASARI_TANIM> basarilar = new List<BASARI_TANIM>();

    public int ToplamPuan()
    {
        int toplam = 0;
        foreach (var b in basarilar) toplam += b.puan;
        return toplam;
    }
}

public static class BASARI_RENK
{
    public static readonly Color Kilitli = new Color(0.35f, 0.35f, 0.35f, 1f);

    public static Color Get(BASARI_KADEME kademe)
    {
        switch (kademe)
        {
            case BASARI_KADEME.Bronz: return new Color32(205, 127,  50, 255);
            case BASARI_KADEME.Gumus: return new Color32(200, 200, 210, 255);
            case BASARI_KADEME.Altin: return new Color32(255, 204,   0, 255);
            case BASARI_KADEME.Elmas: return new Color32(110, 220, 255, 255);
            default:                  return new Color32(190, 120, 255, 255);
        }
    }
}
