using UnityEngine;

public static class KART_SATIS
{
    static readonly string[] _keys =
    {
        "elli", "yetmisBes", "yuz", "yuzYirmiBes", "yuzElli", "yuzYetmisBes",
        "ikiYuzMavi", "ikiYuzKirmizi", "ikiYuzElliMavi", "ikiYuzElliKirmizi",
        "ucYuzMavi", "ucYuzKirmizi", "dortYuzMavi", "dortYuzKirmizi",
        "besYuzMavi", "besYuzKirmizi", "altiYuzMavi", "altiYuzKirmizi",
        "yediYuzElliMavi", "yediYuzElliKirmizi", "binMavi", "binKirmizi"
    };

    static readonly int[] _defaults =
    {
        -1, -1, 50, 75, 100, 125, 150, 175, 200, 200,
        300, 300, 400, 400, 500, 500, 600, 600, 750, 750, 1000, 1000
    };

    public static int  GetPrice(int index)     => PlayerPrefs.GetInt(_keys[index], _defaults[index]);
    public static void SetSold(int index)      => PlayerPrefs.SetInt(_keys[index], -1);
    public static int  GetAktifKart()          => PlayerPrefs.GetInt("aktifKart", 0);
    public static void SetAktifKart(int deger) => PlayerPrefs.SetInt("aktifKart", deger);
}
