using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartDestesi : MonoBehaviour
{
    [SerializeField] Sprite[] cardsBack;
    [SerializeField] Sprite[] cardsSinek;
    [SerializeField] Sprite[] cardsKupa;
    [SerializeField] Sprite[] cardsKaro;
    [SerializeField] Sprite[] cardsMaca;


    public Sprite[] GetOnuclu(KartTur kartTur)
    {
        switch (kartTur)
        {
            case KartTur.Kupa:
                return cardsKupa;

            case KartTur.Maca:
                return cardsMaca;
            case KartTur.Karo:
                return cardsKaro;
            case KartTur.Sinek:
            default:
                return cardsSinek;
        }
    }

    public Sprite GetBackOfCardRandom()
    {
        return cardsBack[UnityEngine.Random.Range(0, cardsBack.Length)];
    }
    public Sprite GetBackOfCard(int index) { return cardsBack[index]; }
    public Sprite GetKart(KartTur kartTur, int index)
    {
        switch (kartTur)
        {
            case KartTur.Kupa:
                return cardsKupa[index];

            case KartTur.Maca:
                return cardsMaca[index];
            case KartTur.Karo:
                return cardsKaro[index];
            case KartTur.Sinek:
            default:
                return cardsSinek[index];
        }
    }
    public Sprite GetKart(string kart)

    {
        KartTur kartTur;
        int index = Convert.ToInt32(kart.Substring(2, 2));
        switch (kart.Substring(0, 2))
        {

            case "MA":kartTur = KartTur.Maca;break;
            case "SI":kartTur = KartTur.Sinek;break;
            case "KU":kartTur = KartTur.Kupa;break;
            case "KA":
            default:
                kartTur = KartTur.Karo;
                break;
        }

        return GetKart(kartTur, index-1);

    }


}
