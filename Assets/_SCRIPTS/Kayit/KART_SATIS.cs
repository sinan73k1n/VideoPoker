using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KART_SATIS : MonoBehaviour
{
    const string elli = "elli";
    const string yetmisBes = "yetmisBes";
    const string yuz = "yuz";
    const string yuzYirmiBes = "yuzYirmiBes";
    const string yuzElli = "yuzElli";
    const string yuzYetmisBes = "yuzYetmisBes";
    const string ikiYuzMavi = "ikiYuzMavi";
    const string ikiYuzKirmizi= "ikiYuzKirmizi";
    const string ikiYuzElliMavi = "ikiYuzElliMavi";
    const string ikiYuzElliKirmizi = "ikiYuzElliKirmizi";
    const string ucYuzMavi = "ucYuzMavi";
    const string ucYuzKirmizi = "ucYuzKirmizi";
    const string dortYuzMavi = "dortYuzMavi";
    const string dortYuzKirmizi = "dortYuzKirmizi";
    const string besYuzMavi = "besYuzMavi";
    const string besYuzKirmizi = "besYuzKirmizi";
    const string altiYuzMavi = "altiYuzMavi";
    const string altiYuzKirmizi = "altiYuzKirmizi";
    const string yediYuzElliMavi = "yediYuzElliMavi";
    const string yediYuzElliKirmizi = "yediYuzElliKirmizi";
    const string binMavi = "binMavi";
    const string binKirmizi = "binKirmizi";

    const string aktifKart = "aktifKart";

    public static void SetAktifKart(int deger) { PlayerPrefs.SetInt(aktifKart, deger); }
    public static int GetAktifKart() {return PlayerPrefs.GetInt(aktifKart, 0); }

    public static void SatildiElli  () { PlayerPrefs.SetInt(elli, -1); }
    public static void SatildiyetmisBes    ()  { PlayerPrefs.SetInt(yetmisBes, -1); }
    public static void Satildiyuz          ()        { PlayerPrefs.SetInt(yuz, -1); }
    public static void SatildiyuzYirmiBes  () { PlayerPrefs.SetInt(yuzYirmiBes, -1); }
    public static void SatildiyuzElli() { PlayerPrefs.SetInt(yuzElli, -1); }
    public static void SatildiyuzYetmisBes() { PlayerPrefs.SetInt(yuzYetmisBes, -1); }
    public static void SatildiikiYuzMavi() { PlayerPrefs.SetInt(ikiYuzMavi, -1); }
    public static void SatildiikiYuzKirmizi() { PlayerPrefs.SetInt(ikiYuzKirmizi, -1); }
    public static void SatildiikiYuzElliMavi() { PlayerPrefs.SetInt(ikiYuzElliMavi, -1); }
    public static void SatildiikiYuzElliKirmizi() { PlayerPrefs.SetInt(ikiYuzElliKirmizi, -1); }
    public static void SatildiucYuzMavi() { PlayerPrefs.SetInt(ucYuzMavi, -1); }
    public static void SatildiucYuzKirmizi() { PlayerPrefs.SetInt(ucYuzKirmizi, -1); }
    public static void SatildidortYuzMavi() { PlayerPrefs.SetInt(dortYuzMavi, -1); }
    public static void SatildidortYuzKirmizi() { PlayerPrefs.SetInt(dortYuzKirmizi, -1); }
    public static void SatildibesYuzMavi() { PlayerPrefs.SetInt(besYuzMavi, -1); }
    public static void SatildibesYuzKirmizi() { PlayerPrefs.SetInt(besYuzKirmizi, -1); }
    public static void SatildibaltiYuzMavi() { PlayerPrefs.SetInt(altiYuzMavi, -1); }
    public static void SatildialtiYuzKirmizi() { PlayerPrefs.SetInt(altiYuzKirmizi, -1); }
    public static void SatildiyediYuzElliMavi() { PlayerPrefs.SetInt(yediYuzElliMavi, -1); }
    public static void SatildiyediYuzElliKirmizi() { PlayerPrefs.SetInt(yediYuzElliKirmizi, -1); }
    public static void SatildibinMavi() { PlayerPrefs.SetInt(binMavi, -1); }
    public static void SatildibinKirmizi() { PlayerPrefs.SetInt(binKirmizi, -1); }

    public static int GetElli() { return PlayerPrefs.GetInt(elli, -1); }
    public static int GetyetmisBes() { return PlayerPrefs.GetInt(yetmisBes, -1); }
    public static int Getyuz() { return PlayerPrefs.GetInt(yuz, 50); }
    public static int GetyuzYirmiBes() { return PlayerPrefs.GetInt(yuzYirmiBes, 75); }
    public static int GetyuzElli() { return PlayerPrefs.GetInt(yuzElli, 100); }
    public static int GetyuzYetmisBes() { return PlayerPrefs.GetInt(yuzYetmisBes, 125); }
    public static int GetikiYuzMavi() { return PlayerPrefs.GetInt(ikiYuzMavi, 150); }
    public static int GetikiYuzKirmizi() { return PlayerPrefs.GetInt(ikiYuzKirmizi, 175); }
    public static int GetikiYuzElliMavi() { return PlayerPrefs.GetInt(ikiYuzElliMavi, 200); }
    public static int GetikiYuzElliKirmizi() { return PlayerPrefs.GetInt(ikiYuzElliKirmizi, 200); }
    public static int GetucYuzMavi() { return PlayerPrefs.GetInt(ucYuzMavi, 300); }
    public static int GetucYuzKirmizi() { return PlayerPrefs.GetInt(ucYuzKirmizi, 300); }
    public static int GetdortYuzMavi() { return PlayerPrefs.GetInt(dortYuzMavi, 400); }
    public static int GetdortYuzKirmizi() { return PlayerPrefs.GetInt(dortYuzKirmizi, 400); }
    public static int GetbesYuzMavi() { return PlayerPrefs.GetInt(besYuzMavi, 500); }
    public static int GetbesYuzKirmizi() { return PlayerPrefs.GetInt(besYuzKirmizi, 500); }
    public static int GetbaltiYuzMavi() { return PlayerPrefs.GetInt(altiYuzMavi, 600); }
    public static int GetaltiYuzKirmizi() { return PlayerPrefs.GetInt(altiYuzKirmizi, 600); }
    public static int GetyediYuzElliMavi() { return PlayerPrefs.GetInt(yediYuzElliMavi, 750); }
    public static int GetyediYuzElliKirmizi() { return PlayerPrefs.GetInt(yediYuzElliKirmizi, 750); }
    public static int GetbinMavi() { return PlayerPrefs.GetInt(binMavi, 1000); }
    public static int GetbinKirmizi() { return PlayerPrefs.GetInt(binKirmizi, 1000); }
}
