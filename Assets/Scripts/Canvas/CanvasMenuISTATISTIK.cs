using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CanvasMenuISTATISTIK : MonoBehaviour
{

    [SerializeField]
    TMP_Text _txtRoyalFlus, _txtStraightFlush, _txtFourAKIND, _txtFullHouse,
     _txtFlush, _txtStraight, _txtThreeAKIND, _txtTwoPair, _txtJackOrBetter, _txtGame, _txtMaxCredit, _txtWin;
    [SerializeField] Button _btnClose;
    [SerializeField] Image[] _imgsRoyalFlusj,_imgsStagihtFlush,_imgsFourOf,_imgsFullH,_imgsFlush,_imgsStraight,_imgsThreeOf,_imgsTwoP,imgsJack;
    KartDestesi kartDestesi;
    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 20;
        kartDestesi = FindObjectOfType<KartDestesi>();
    }

    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
        AtaTextVar();
        AtaKartlar();
    }

    private void AtaKartlar()
    {
        SetEl(_imgsRoyalFlusj, KAYIT.GetSON_EL_ROYAL_FLUSH());
        SetEl(_imgsStagihtFlush, KAYIT.GetSON_EL_STRAIGHT_FLUSH());
        SetEl(_imgsFourOf, KAYIT.GetSON_EL_FOUR_A_KIND());
        SetEl(_imgsFullH, KAYIT.GetSON_EL_FULL_HOUSE());
        SetEl(_imgsFlush, KAYIT.GetSON_EL_FLUSH());
        SetEl(_imgsStraight, KAYIT.GetSSON_EL_STRAIGHTH());
        SetEl(_imgsThreeOf, KAYIT.GetSON_EL_THREE_A_KIND());
        SetEl(_imgsTwoP, KAYIT.GetSON_EL_TWO_PAIR());
        SetEl(imgsJack, KAYIT.GetSON_EL_JACK_OR_BETTER());
    }

    void SetEl(Image[] imgs,List<string> list)
    {
        if (list[0] == "0000") return;
        for (int i = 0; i < 5; i++)
        {
            imgs[i].sprite = kartDestesi.GetKart(list[i]);
        }
    }

    void AtaTextVar()
    {
        _txtRoyalFlus.text = KAYIT.GetCountOfHand(TypeOfPokerHand.RoyalFlush).ToString();
        _txtStraightFlush.text = KAYIT.GetCountOfHand(TypeOfPokerHand.StraightFlush).ToString();
        _txtFourAKIND.text = KAYIT.GetCountOfHand(TypeOfPokerHand.FourOfAKind).ToString();
        _txtFullHouse.text = KAYIT.GetCountOfHand(TypeOfPokerHand.FullHouse).ToString();
        _txtFlush.text = KAYIT.GetCountOfHand(TypeOfPokerHand.Flush).ToString();
        _txtStraight.text = KAYIT.GetCountOfHand(TypeOfPokerHand.Straight).ToString();
        _txtThreeAKIND.text = KAYIT.GetCountOfHand(TypeOfPokerHand.ThreeOfAKind).ToString();
        _txtTwoPair.text = KAYIT.GetCountOfHand(TypeOfPokerHand.TwoPair).ToString();
        _txtJackOrBetter.text = KAYIT.GetCountOfHand(TypeOfPokerHand.JackOrBetter).ToString();
        _txtGame.text = KAYIT.GetCountOf_GAME().ToString();
        //_txtMaxCredit.text = KAYIT.GetWinCountOf_MAX_CREDIT().ToString();
        _txtWin.text = KAYIT.GetWinCountOf_WIN().ToString();
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }



}