#if UNITY_EDITOR
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEditor;
using UnityEngine;

// Play Console → Play Games Services → Hedefler → "İçe aktar" için zip üretir.
// Biçim (developer.android.com/games/pgs/integrate-achievements, ⏱ 2026-10-13):
//   AchievementsMetadata.csv     başlıksız · Name,Description,Incremental,Steps,Initial State,Points,List Order
//   AchievementsIconsMappings.csv başlıksız · Name,icon dosyası
//   ikonlar 512×512 PNG, alt klasör yok.
// ⛔ Play'de yayınlandıktan sonra tip (ilerlemeli/tek) ve gizlilik değişmez, başarı silinmez.
public static class BASARI_PLAY_PAKETI
{
    const string LISTE = "Assets/ScriptableObjects/BASARI_LISTESI.asset";
    public const string CIKTI = "PlayConsole/VideoPoker-basarilar.zip";   // proje kökü; .gitignore'da

    [MenuItem("Video Poker/Play Console Basari Zip Uret")]
    public static void Uret()
    {
        var liste = AssetDatabase.LoadAssetAtPath<BASARI_LISTESI>(LISTE);
        if (liste == null) { Debug.LogError("[BASARI] liste yok: " + LISTE); return; }

        var meta = new StringBuilder();
        var ikon = new StringBuilder();
        int hata = 0;
        for (int i = 0; i < liste.basarilar.Count; i++)
        {
            var b = liste.basarilar[i];
            if (b.ad.Contains(",") || b.aciklama.Contains(",")) { Debug.LogError($"[BASARI] '{b.id}' virgül içeriyor"); hata++; }
            if (b.puan < 5 || b.puan > 200 || b.puan % 5 != 0) { Debug.LogError($"[BASARI] '{b.id}' puanı {b.puan} (5'in katı, 5–200)"); hata++; }
            if (!File.Exists(IkonYolu(b))) { Debug.LogError($"[BASARI] '{b.id}' ikonu yok → Video Poker/Basari Ikonlari Uret"); hata++; }

            meta.Append(b.ad).Append(',')
                .Append(b.aciklama).Append(',')
                .Append(b.PlayArtimli ? "True" : "False").Append(',')
                .Append(b.PlayArtimli ? b.hedef.ToString() : "").Append(',')
                .Append(b.gizli ? "Hidden" : "Revealed").Append(',')
                .Append(b.puan).Append(',')
                .Append(i + 1).Append('\n');
            ikon.Append(b.ad).Append(',').Append(b.id).Append(".png\n");
        }
        if (liste.ToplamPuan() > 2000) { Debug.LogError($"[BASARI] toplam puan {liste.ToplamPuan()} > 2000"); hata++; }
        if (hata > 0) { Debug.LogError($"[BASARI] zip ÜRETİLMEDİ: {hata} hata"); return; }

        Directory.CreateDirectory(Path.GetDirectoryName(CIKTI));
        if (File.Exists(CIKTI)) File.Delete(CIKTI);
        var utf8 = new UTF8Encoding(false);
        using (var zip = new ZipArchive(File.Create(CIKTI), ZipArchiveMode.Create))
        {
            Yaz(zip, "AchievementsMetadata.csv", utf8.GetBytes(meta.ToString()));
            Yaz(zip, "AchievementsIconsMappings.csv", utf8.GetBytes(ikon.ToString()));
            foreach (var b in liste.basarilar) Yaz(zip, b.id + ".png", File.ReadAllBytes(IkonYolu(b)));
        }
        Debug.Log($"[BASARI] Play zip: {liste.basarilar.Count} başarı, {liste.ToplamPuan()} puan → {Path.GetFullPath(CIKTI)}");
        EditorUtility.RevealInFinder(CIKTI);
    }

    static string IkonYolu(BASARI_TANIM b) => $"{BASARI_IKON_URETICI.KLASOR}/{b.id}.png";

    static void Yaz(ZipArchive zip, string ad, byte[] veri)
    {
        using (var s = zip.CreateEntry(ad, System.IO.Compression.CompressionLevel.Optimal).Open())
            s.Write(veri, 0, veri.Length);
    }
}
#endif
