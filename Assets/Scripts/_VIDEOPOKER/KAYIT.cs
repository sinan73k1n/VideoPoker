using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAYIT : MonoBehaviour
{

    const string ANA_BAKIYE = "AnaBakiye";
    const string SECILI_BAHIS = "SeciliBahis";

    public static int GetAnaBakiye() { return PlayerPrefs.GetInt(ANA_BAKIYE, 50); }
    public static void AddToAnaBakiye(int neKadar) { PlayerPrefs.SetInt(ANA_BAKIYE, GetAnaBakiye() + neKadar); }

    public static int GetSeciliBahis() { return PlayerPrefs.GetInt(SECILI_BAHIS, 1); }
    public static void SetSeciliBahisiBirArttir() {


        int bahis= GetSeciliBahis();
        bahis++;
        if (bahis >= 6) { bahis = 1; }
        PlayerPrefs.SetInt(SECILI_BAHIS,bahis );

    }


}
