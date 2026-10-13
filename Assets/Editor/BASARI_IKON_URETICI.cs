#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// Başarı ikonlarını oyunun kendi sprite'larından üretir: 512×512 PNG (Play Console bunu ister).
// Aynı dosya oyun içinde de kullanılır; oyuna 128 px'e küçültülerek girer (45 ikon × 512² bellek şişirmesin).
// Play kuralları: ikonda YAZI yok, Play daire içinde gösterir → içerik merkezdeki daireye sığar.
public static class BASARI_IKON_URETICI
{
    const string LISTE   = "Assets/ScriptableObjects/BASARI_LISTESI.asset";
    const string DESTE   = "Assets/_PREFABS/Oyun_Elemanlari/KartDestesi.prefab";
    const string KUPALAR = "Assets/_IMG/iconKartKupa.png";
    const string YILDIZ  = "Assets/_IMG/Yildiz.png";
    public const string KLASOR = "Assets/_IMG/Basari";
    const int BOY = 512;

    static readonly Color ZEMIN    = new Color(0f, 0.03f, 0.45f, 1f);
    static readonly Color ZEMIN_IC = new Color(0f, 0.043f, 0.783f, 1f);   // oyunun pencere mavisi
    static readonly Color SARI     = new Color32(255, 204, 0, 255);          // Yildiz.png beyaz → oyunun sarısına boyanır

    static readonly Dictionary<Texture2D, Texture2D> _okunur = new Dictionary<Texture2D, Texture2D>();
    static KartDestesi _deste;
    static Sprite[] _kupa = new Sprite[4];
    static Sprite _yildiz;

    [MenuItem("Video Poker/Basari Ikonlari Uret")]
    public static void Uret()
    {
        var liste = AssetDatabase.LoadAssetAtPath<BASARI_LISTESI>(LISTE);
        if (liste == null) { Debug.LogError("[BASARI] liste yok: " + LISTE); return; }
        if (!Hazirla()) return;
        Directory.CreateDirectory(KLASOR);

        try
        {
            for (int i = 0; i < liste.basarilar.Count; i++)
            {
                var b = liste.basarilar[i];
                EditorUtility.DisplayProgressBar("Başarı ikonları", b.id, i / (float)liste.basarilar.Count);
                File.WriteAllBytes($"{KLASOR}/{b.id}.png", Ciz(b).EncodeToPNG());
            }
        }
        finally { EditorUtility.ClearProgressBar(); }

        AssetDatabase.Refresh();
        foreach (var b in liste.basarilar)
        {
            string yol = $"{KLASOR}/{b.id}.png";
            var imp = (TextureImporter)AssetImporter.GetAtPath(yol);
            imp.textureType = TextureImporterType.Sprite;
            imp.spriteImportMode = SpriteImportMode.Single;   // yalnız bu üretilen dosyalar; sheet'lere dokunulmaz
            imp.maxTextureSize = 128;
            imp.mipmapEnabled = false;
            imp.alphaIsTransparency = true;
            imp.SaveAndReimport();
            b.ikon = AssetDatabase.LoadAssetAtPath<Sprite>(yol);
        }
        EditorUtility.SetDirty(liste);
        AssetDatabase.SaveAssets();
        foreach (var t in _okunur.Values) Object.DestroyImmediate(t);
        _okunur.Clear();
        Debug.Log($"[BASARI] {liste.basarilar.Count} ikon → {KLASOR}");
    }

    static bool Hazirla()
    {
        var go = AssetDatabase.LoadAssetAtPath<GameObject>(DESTE);
        _deste = go != null ? go.GetComponent<KartDestesi>() : null;
        foreach (var o in AssetDatabase.LoadAllAssetsAtPath(KUPALAR))
            if (o is Sprite s && s.name.StartsWith("iconKartKupa_"))
            {
                int n = int.Parse(s.name.Substring("iconKartKupa_".Length));
                if (n < 4) _kupa[n] = s;   // 0..3: sadeden süslüye dört kupa
            }
        _yildiz = AssetDatabase.LoadAssetAtPath<Sprite>(YILDIZ);
        bool tamam = _deste != null && _kupa[3] != null && _yildiz != null;
        if (!tamam) Debug.LogError("[BASARI] ikon kaynakları eksik (KartDestesi / iconKartKupa / Yildiz)");
        return tamam;
    }

    // ── Sahne ───────────────────────────────────────────────────────────────

    static Texture2D Ciz(BASARI_TANIM b)
    {
        var tuval = new Texture2D(BOY, BOY, TextureFormat.RGBA32, false);
        var px = new Color[BOY * BOY];
        Color halka = BASARI_RENK.Get(b.kademe);
        float m = (BOY - 1) / 2f;
        for (int y = 0; y < BOY; y++)
            for (int x = 0; x < BOY; x++)
            {
                float r = Mathf.Sqrt((x - m) * (x - m) + (y - m) * (y - m));
                px[y * BOY + x] = r > 250 ? ZEMIN : r > 222 ? halka : r > 214 ? ZEMIN : ZEMIN_IC;
            }

        int kademe = Mathf.Clamp((int)b.kademe, 0, 3);
        switch (b.id.Split('_')[0])
        {
            case "el":        Arkalar(px, new[] { 0, 0, 0, 0 }); break;
            case "job":       Kartlar(px, "MA11", "KU11"); break;
            case "ikiper":    Kartlar(px, "MA08", "KU08", "SI03", "KA03"); break;
            case "uclu":      Kartlar(px, "MA07", "KU07", "KA07"); break;
            case "kenta":     Kartlar(px, "SI05", "KA06", "KU07", "MA08", "SI09"); break;
            case "flos":      Kartlar(px, "KA02", "KA05", "KA08", "KA11", "KA13"); break;
            case "fulev":     Kartlar(px, "MA13", "KU13", "KA13", "SI04", "KU04"); break;
            case "kare":      Kartlar(px, "MA09", "KU09", "KA09", "SI09"); break;
            case "renkkenta": Kartlar(px, "MA05", "MA06", "MA07", "MA08", "MA09"); break;
            case "royal":     Kartlar(px, "KU10", "KU11", "KU12", "KU13", "KU01"); break;
            case "kazanc":    Yerlestir(px, _yildiz, m, m, 300, 300, SARI); break;
            case "seri":      for (int i = 0; i < 3; i++) Yerlestir(px, _yildiz, m + (i - 1) * 118, m + (i == 1 ? 40 : -10), 150, 150, SARI); break;
            case "gorev":     Yerlestir(px, _kupa[kademe], m, m, 320, 320); break;
            case "kart":      Arkalar(px, new[] { 0, 5, 10, 15, 20 }); break;
            case "ozel":
                if (b.id == "ozel_dort_as")     Kartlar(px, "MA01", "KU01", "KA01", "SI01");
                else if (b.id == "ozel_hazir_royal") Kartlar(px, "MA10", "MA11", "MA12", "MA13", "MA01");
                else { Kartlar(px, "KU10", "KU11", "KU12", "KU13", "KU01"); Yerlestir(px, _yildiz, m, m + 150, 120, 120, SARI); }
                break;
        }

        tuval.SetPixels(px);
        tuval.Apply();
        return tuval;
    }

    // Kart yelpazesi: döndürme yok (piksel sanatı bozulur), hafif kavisle üst üste.
    static void Kartlar(Color[] px, params string[] kodlar)
    {
        int n = kodlar.Length;
        float w = n >= 5 ? 128 : 150, h = w * 255f / 175f;
        float adim = n >= 5 ? 56 : 74;
        float m = (BOY - 1) / 2f, bas = m - adim * (n - 1) / 2f;
        for (int i = 0; i < n; i++)
        {
            float t = n == 1 ? 0 : i / (float)(n - 1) * 2 - 1;   // -1..1
            Yerlestir(px, _deste.GetKart(kodlar[i]), bas + adim * i, m + 18 - 22 * t * t, w, h);
        }
    }

    static void Arkalar(Color[] px, int[] indeksler)
    {
        int n = indeksler.Length;
        float w = 140, h = w * 255f / 175f, adim = n >= 5 ? 52 : 40;
        float m = (BOY - 1) / 2f, bas = m - adim * (n - 1) / 2f;
        for (int i = 0; i < n; i++)
            Yerlestir(px, _deste.GetBackOfCard(indeksler[i]), bas + adim * i, m + (n >= 5 ? 0 : (i - (n - 1) / 2f) * 10), w, h);
    }

    // Sprite'ı (cx,cy) merkezli w×h kutuya çizer (bilinear örnekleme + alfa karışımı).
    static void Yerlestir(Color[] px, Sprite s, float cx, float cy, float w, float h) => Yerlestir(px, s, cx, cy, w, h, Color.white);

    static void Yerlestir(Color[] px, Sprite s, float cx, float cy, float w, float h, Color boya)
    {
        var kaynak = Okunur(s.texture);
        Rect r = s.textureRect;
        int x0 = Mathf.FloorToInt(cx - w / 2), y0 = Mathf.FloorToInt(cy - h / 2);
        for (int y = 0; y < Mathf.CeilToInt(h); y++)
            for (int x = 0; x < Mathf.CeilToInt(w); x++)
            {
                int dx = x0 + x, dy = y0 + y;
                if (dx < 0 || dy < 0 || dx >= BOY || dy >= BOY) continue;
                float u = (r.x + (x + 0.5f) / w * r.width) / kaynak.width;
                float v = (r.y + (y + 0.5f) / h * r.height) / kaynak.height;
                Color c = kaynak.GetPixelBilinear(u, v) * boya;
                if (c.a <= 0.01f) continue;
                int i = dy * BOY + dx;
                px[i] = Color.Lerp(px[i], new Color(c.r, c.g, c.b, 1f), c.a);
            }
    }

    // Sheet'ler okunamaz (isReadable 0) → import ayarına dokunmadan GPU üzerinden okunur kopya alınır.
    static Texture2D Okunur(Texture2D t)
    {
        if (_okunur.TryGetValue(t, out var hazir)) return hazir;
        var rt = RenderTexture.GetTemporary(t.width, t.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        var once = RenderTexture.active;
        Graphics.Blit(t, rt);
        RenderTexture.active = rt;
        var kopya = new Texture2D(t.width, t.height, TextureFormat.RGBA32, false);
        kopya.ReadPixels(new Rect(0, 0, t.width, t.height), 0, 0);
        kopya.Apply();
        RenderTexture.active = once;
        RenderTexture.ReleaseTemporary(rt);
        _okunur[t] = kopya;
        return kopya;
    }
}
#endif
