#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class GameBalanceWindow : EditorWindow
{
    [MenuItem("Video Poker/Game Balance & Debug")]
    public static void ShowWindow() => GetWindow<GameBalanceWindow>("VP Balance");

    Vector2 _scroll;
    int _creditAmount = 100;
    IntVariable _creditsAsset;
    BASARI_LISTESI _basariListesi;

    static readonly string[] _sahteElAdlari = { "Royal Flush", "Straight Flush", "Four of a Kind", "Full House", "Flush", "Straight", "Three of a Kind", "Two Pair", "Jacks or Better", "Kayıp" };
    static readonly int[]    _sahteElCarpan = { 250, 50, 25, 9, 6, 4, 3, 2, 1, 0 };
    int  _sahteEl, _sahteBahis = 1;
    bool _sahteHazir, _sahteDortAs;

    void OnEnable()
    {
        var guids = UnityEditor.AssetDatabase.FindAssets("t:IntVariable");
        foreach (var g in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(g);
            if (path.Contains("Credits"))
            {
                _creditsAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<IntVariable>(path);
                break;
            }
        }
    }

    void OnGUI()
    {
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        DrawCredits();
        EditorGUILayout.Space(8);
        DrawHandStats();
        EditorGUILayout.Space(8);
        DrawBasarilar();
        EditorGUILayout.Space(8);
        DrawDangerZone();

        EditorGUILayout.EndScrollView();
    }

    void DrawCredits()
    {
        EditorGUILayout.LabelField("CREDITS & BET", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        int credits = PlayerPrefs.GetInt("AnaBakiye", 50);
        int bet     = PlayerPrefs.GetInt("SeciliBahis", 1);

        EditorGUILayout.LabelField("Current Credits", credits.ToString());
        EditorGUILayout.LabelField("Current Bet",     bet.ToString() + "$");

        EditorGUILayout.Space(4);
        _creditAmount = EditorGUILayout.IntField("Amount to Add", _creditAmount);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Credits"))
        {
            PlayerPrefs.SetInt("AnaBakiye", credits + _creditAmount);
            PlayerPrefs.Save();
            Repaint();
        }
        if (GUILayout.Button("Set to 50 (Default)"))
        {
            PlayerPrefs.SetInt("AnaBakiye", 50);
            PlayerPrefs.Save();
            Repaint();
        }
        if (GUILayout.Button("Set to 9999"))
        {
            PlayerPrefs.SetInt("AnaBakiye", 9999);
            PlayerPrefs.Save();
            Repaint();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;
    }

    void DrawHandStats()
    {
        EditorGUILayout.LabelField("HAND STATISTICS", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        DrawStat("Royal Flush    (250x)",  "OYNANAN_ROYAL_FLUSH");
        DrawStat("Straight Flush  (50x)",  "OYNANAN_STRAIGHT_FLUSH");
        DrawStat("Four of a Kind  (25x)",  "OYNANAN_FOUR_A_KIND");
        DrawStat("Full House       (9x)",  "OYNANAN_FULL_HOUSE");
        DrawStat("Flush            (6x)",  "OYNANAN_FLUSH");
        DrawStat("Straight         (4x)",  "OYNANAN_STRAIGHT");
        DrawStat("Three of a Kind  (3x)",  "OYNANAN_THREE_A_KIND");
        DrawStat("Two Pair         (2x)",  "OYNANAN_TWO_PAIR");
        DrawStat("Jacks or Better  (1x)",  "OYNANAN_JACK_OR_BETTER");

        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Total Games",  PlayerPrefs.GetInt("OYNANAN_GAME", 0).ToString());
        EditorGUILayout.LabelField("Total Wins",   PlayerPrefs.GetInt("OYNANAN_WIN",  0).ToString());

        int games = PlayerPrefs.GetInt("OYNANAN_GAME", 0);
        int wins  = PlayerPrefs.GetInt("OYNANAN_WIN",  0);
        if (games > 0)
        {
            float pct = (wins / (float)games) * 100f;
            EditorGUILayout.LabelField("Win Rate", pct.ToString("F1") + "%");
        }

        EditorGUI.indentLevel--;
    }

    void DrawBasarilar()
    {
        EditorGUILayout.LabelField("ACHIEVEMENTS", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        if (_basariListesi == null)
            _basariListesi = AssetDatabase.LoadAssetAtPath<BASARI_LISTESI>("Assets/ScriptableObjects/BASARI_LISTESI.asset");

        EditorGUILayout.LabelField("Toplam Kazanç", KAYIT_BASARI.GetToplamKazanc().ToString());
        EditorGUILayout.LabelField("Toplam Bahis",  KAYIT_BASARI.GetToplamBahis().ToString());
        EditorGUILayout.LabelField("Seri / En iyi", $"{KAYIT_BASARI.GetSeri()} / {KAYIT_BASARI.GetSeriEnIyi()}");
        EditorGUILayout.LabelField("Görev ödülü",   KAYIT_BASARI.GetGorevOdulSayisi().ToString());
        EditorGUILayout.LabelField("Kart arkası",   KAYIT_BASARI.GetAcilanKartArkasi().ToString());

        if (_basariListesi == null)
        {
            EditorGUILayout.HelpBox("BASARI_LISTESI.asset yok → Video Poker/Basari Listesi Uret", MessageType.Info);
        }
        else
        {
            int acik = 0;
            foreach (var b in _basariListesi.basarilar) if (KAYIT_BASARI.GetAcik(b.id)) acik++;
            EditorGUILayout.LabelField("Açık", $"{acik}/{_basariListesi.basarilar.Count}  ·  {_basariListesi.ToplamPuan()} puan");

            if (GUILayout.Button("Sıfırla: Başarılar (sayaçlar kalır)") &&
                EditorUtility.DisplayDialog("Emin misin?", "Açılmış bütün başarılar kilitlenecek.", "Sıfırla", "İptal"))
            {
                foreach (var b in _basariListesi.basarilar) KAYIT_BASARI.Kilitle(b.id);
                foreach (BASARI_SAYAC s in System.Enum.GetValues(typeof(BASARI_SAYAC))) PlayerPrefs.DeleteKey("BASARI_OZEL_" + s);
                PlayerPrefs.DeleteKey("BASARI_ILK_TARAMA");
                PlayerPrefs.Save();
            }
        }

        EditorGUILayout.Space(4);
        _sahteEl     = EditorGUILayout.Popup("Sahte el", _sahteEl, _sahteElAdlari);
        _sahteBahis  = EditorGUILayout.IntSlider("Bahis", _sahteBahis, 1, 5);
        _sahteHazir  = EditorGUILayout.Toggle("İlk dağıtımda hazırdı", _sahteHazir);
        _sahteDortAs = EditorGUILayout.Toggle("Kare = dört As", _sahteDortAs);

        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Sahte El Gönder (Play mode)"))
        {
            int carpan = _sahteElCarpan[_sahteEl];
            int kazanc = carpan * _sahteBahis;
            if (kazanc == 1250) kazanc = 4000;
            if (carpan > 0) KAYIT.AddOneHand((TypeOfPokerHand)_sahteEl);
            OYUN_OLAYLARI.ElBittiBildir(new EL_SONUCU
            {
                carpan           = carpan,
                bahis            = _sahteBahis,
                kazanc           = kazanc,
                kartlar          = _sahteDortAs ? "KU01MA01SI01KA01KU05" : "",
                ilkDagitimCarpan = _sahteHazir ? carpan : 0
            });
        }
        GUI.enabled = true;

        EditorGUI.indentLevel--;
    }

    void DrawStat(string label, string key)
    {
        int count = PlayerPrefs.GetInt(key, 0);
        EditorGUILayout.LabelField(label, count.ToString());
    }

    void DrawDangerZone()
    {
        EditorGUILayout.LabelField("RESET / DEBUG", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        EditorGUILayout.HelpBox("Bu işlemler geri alınamaz.", MessageType.Warning);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Sıfırla: İstatistikler"))
        {
            if (EditorUtility.DisplayDialog("Emin misin?", "Tüm istatistikler sıfırlanacak.", "Sıfırla", "İptal"))
            {
                string[] statKeys = {
                    "OYNANAN_ROYAL_FLUSH","OYNANAN_STRAIGHT_FLUSH","OYNANAN_FOUR_A_KIND",
                    "OYNANAN_FULL_HOUSE","OYNANAN_FLUSH","OYNANAN_STRAIGHT",
                    "OYNANAN_THREE_A_KIND","OYNANAN_TWO_PAIR","OYNANAN_JACK_OR_BETTER",
                    "OYNANAN_GAME","OYNANAN_WIN"
                };
                foreach (var k in statKeys) PlayerPrefs.DeleteKey(k);
                PlayerPrefs.Save();
                Repaint();
            }
        }
        if (GUILayout.Button("Sıfırla: Tüm Veriler"))
        {
            if (EditorUtility.DisplayDialog("Emin misin?", "TÜM PlayerPrefs silinecek!", "Sıfırla", "İptal"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Repaint();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (Application.isPlaying)
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.HelpBox("Play mode aktif — değişiklikler anında yansımaz, oyunu yeniden başlat.", MessageType.Info);
        }

        EditorGUI.indentLevel--;
    }

    void OnInspectorUpdate() => Repaint();
}
#endif
