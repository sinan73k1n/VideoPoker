#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class GameBalanceWindow : EditorWindow
{
    [MenuItem("Video Poker/Game Balance & Debug")]
    public static void ShowWindow() => GetWindow<GameBalanceWindow>("VP Balance");

    Vector2 _scroll;
    int _creditAmount = 100;

    void OnGUI()
    {
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        DrawCredits();
        EditorGUILayout.Space(8);
        DrawHandStats();
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
