#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Varsayılan başarı listesini üretir. Liste Play Console'a girildikten sonra BUNU YENİDEN ÇALIŞTIRMA:
// androidId'ler ve elle yapılan ad/ikon değişiklikleri ezilir.
public static class BASARI_LISTESI_URETICI
{
    const string YOL = "Assets/ScriptableObjects/BASARI_LISTESI.asset";

    [MenuItem("Video Poker/Basari Listesi Uret (varsayilan)")]
    public static void Uret()
    {
        var liste = AssetDatabase.LoadAssetAtPath<BASARI_LISTESI>(YOL);
        if (liste != null && !Application.isBatchMode &&
            !EditorUtility.DisplayDialog("Başarı listesi var", "Mevcut liste varsayılanla EZİLECEK (androidId'ler dahil).", "Ez", "İptal"))
            return;

        if (liste == null)
        {
            liste = ScriptableObject.CreateInstance<BASARI_LISTESI>();
            AssetDatabase.CreateAsset(liste, YOL);
        }

        liste.basarilar = Varsayilan();
        EditorUtility.SetDirty(liste);
        AssetDatabase.SaveAssets();
        Debug.Log($"[BASARI] {liste.basarilar.Count} başarı, toplam {liste.ToplamPuan()} puan → {YOL}");
    }

    static List<BASARI_TANIM> Varsayilan()
    {
        var l = new List<BASARI_TANIM>();
        var K = new[] { BASARI_KADEME.Bronz, BASARI_KADEME.Gumus, BASARI_KADEME.Altin, BASARI_KADEME.Elmas };

        // Kademe puanı × nadirlik çarpanı (Play Games: 5'in katı, ≤200, toplam ≤2000)
        void Seri(string on, BASARI_SAYAC sayac, string ad, string fiil, int carpan, params int[] hedefler)
        {
            int[] taban = { 5, 10, 25, 50 };
            for (int i = 0; i < hedefler.Length; i++)
            {
                int h = hedefler[i];
                l.Add(new BASARI_TANIM
                {
                    id       = $"{on}_{h}",
                    ad       = $"{ad} ×{UI_BASARILAR.Sayi(h)}",
                    aciklama = string.Format(fiil, UI_BASARILAR.Sayi(h)),
                    sayac    = sayac,
                    hedef    = h,
                    kademe   = K[i],
                    puan     = Mathf.Min(200, taban[i] * carpan)
                });
            }
        }

        Seri("el",       BASARI_SAYAC.OynananEl,     "HANDS PLAYED",    "Play {0} hands.",                 1, 10, 100, 1000, 10000);
        Seri("job",      BASARI_SAYAC.JacksOrBetter, "JACKS OR BETTER", "Get Jacks or Better {0} times.",  1, 50, 500, 5000);
        Seri("ikiper",   BASARI_SAYAC.TwoPair,       "TWO PAIR",        "Get Two Pair {0} times.",         1, 25, 250, 2500);
        Seri("uclu",     BASARI_SAYAC.ThreeOfAKind,  "THREE OF A KIND", "Get Three of a Kind {0} times.",  1, 10, 100, 1000);
        Seri("kenta",    BASARI_SAYAC.Straight,      "STRAIGHT",        "Get a Straight {0} times.",       1, 5, 50, 500);
        Seri("flos",     BASARI_SAYAC.Flush,         "FLUSH",           "Get a Flush {0} times.",          1, 5, 50, 500);
        Seri("fulev",    BASARI_SAYAC.FullHouse,     "FULL HOUSE",      "Get a Full House {0} times.",     1, 5, 50, 500);
        Seri("kare",     BASARI_SAYAC.FourOfAKind,   "FOUR OF A KIND",  "Get Four of a Kind {0} times.",   2, 1, 10, 100);
        Seri("renkkenta",BASARI_SAYAC.StraightFlush, "STRAIGHT FLUSH",  "Get a Straight Flush {0} times.", 4, 1, 5, 25);
        Seri("royal",    BASARI_SAYAC.RoyalFlush,    "ROYAL FLUSH",     "Get a Royal Flush {0} times.",    6, 1, 3, 10);
        Seri("kazanc",   BASARI_SAYAC.ToplamKazanc,  "TOTAL WINNINGS",  "Win {0} credits from hands.",     1, 1000, 10000, 100000, 1000000);
        Seri("seri",     BASARI_SAYAC.KazancSerisi,  "HOT STREAK",      "Win {0} hands in a row.",         2, 5, 10);
        Seri("gorev",    BASARI_SAYAC.GorevOdulu,    "MISSION REWARDS", "Collect {0} mission rewards.",    1, 10, 100);
        Seri("kart",     BASARI_SAYAC.KartArkasi,    "CARD COLLECTOR",  "Unlock {0} card backs.",          2, 1, 5, 20);

        l.Add(Gizli("ozel_max_royal",  BASARI_SAYAC.OzelMaxBahisRoyal,   "JACKPOT",        "Hit a Royal Flush at max bet (4,000 credits).", 150));
        l.Add(Gizli("ozel_hazir_royal",BASARI_SAYAC.OzelIlkDagitimRoyal, "BORN ROYAL",     "Get a Royal Flush dealt on the first hand.",    150));
        l.Add(Gizli("ozel_dort_as",    BASARI_SAYAC.OzelDortAs,          "FOUR ACES",      "Get Four of a Kind with Aces.",                  25));

        return l;
    }

    static BASARI_TANIM Gizli(string id, BASARI_SAYAC sayac, string ad, string aciklama, int puan) => new BASARI_TANIM
    {
        id = id, ad = ad, aciklama = aciklama, sayac = sayac, hedef = 1,
        kademe = BASARI_KADEME.Gizli, puan = puan, gizli = true
    };
}
#endif
