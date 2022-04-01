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
     _txtFlush, _txtStraight, _txtThreeAKIND, _txtTwoPair, _txtJackOrBetter, _txtGame, _txtMaxCredit, _txtWin, _txtShow;
    [SerializeField] Button _btnClose, _btnShow, _btnRoyalFlusj, _btnStagihtFlush, _btnFourOf, _btnFullH, _btnFlush, _btnStraight, _btnThreeOf, _btnTwoP, _btnJack;
    [SerializeField] Image[] _imgsRoyalFlusj, _imgsStagihtFlush, _imgsFourOf, _imgsFullH, _imgsFlush, _imgsStraight, _imgsThreeOf, _imgsTwoP, imgsJack, _imgsShow;
    KartDestesi kartDestesi;
    [SerializeField] GameObject _goSHOW;

    Sprite _kartArkasi;
    private void Awake()
    {
        _kartArkasi = _imgsRoyalFlusj[0].sprite;
        _goSHOW.SetActive(false);
        GetComponent<Canvas>().sortingOrder = 20;
        kartDestesi = FindObjectOfType<KartDestesi>();
    }

    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
        _btnShow.onClick.AddListener(() => HandleSohw());
        _btnRoyalFlusj.onClick.AddListener(() => SetShow(TypeOfPokerHand.RoyalFlush));
        _btnStagihtFlush.onClick.AddListener(() => SetShow(TypeOfPokerHand.StraightFlush));
        _btnFourOf.onClick.AddListener(() => SetShow(TypeOfPokerHand.FourOfAKind));
        _btnFullH.onClick.AddListener(() => SetShow(TypeOfPokerHand.FullHouse));
        _btnFlush.onClick.AddListener(() => SetShow(TypeOfPokerHand.Flush));
        _btnStraight.onClick.AddListener(() => SetShow(TypeOfPokerHand.Straight));
        _btnThreeOf.onClick.AddListener(() => SetShow(TypeOfPokerHand.ThreeOfAKind));
        _btnTwoP.onClick.AddListener(() => SetShow(TypeOfPokerHand.TwoPair));
        _btnJack.onClick.AddListener(() => SetShow(TypeOfPokerHand.JackOrBetter));

        AtaTextVar();
        AtaKartlar();
    }

    private void HandleSohw()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _goSHOW.SetActive(false);
    }

    void SetShow(TypeOfPokerHand typeOfPokerHand)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _goSHOW.SetActive(true);
        switch (typeOfPokerHand)
        {
            case TypeOfPokerHand.RoyalFlush:
                SetEl(_imgsShow, KAYIT.GetSON_EL_ROYAL_FLUSH());
                _txtShow.text = "ROYAL FLUSH";
                break;
            case TypeOfPokerHand.StraightFlush:
                SetEl(_imgsShow, KAYIT.GetSON_EL_STRAIGHT_FLUSH());
                _txtShow.text = "STRAIGHT FLUSH";
                break;
            case TypeOfPokerHand.FourOfAKind:
                SetEl(_imgsShow, KAYIT.GetSON_EL_FOUR_A_KIND());
                _txtShow.text = "FOUR OF A KIND";
                break;
            case TypeOfPokerHand.FullHouse:
                SetEl(_imgsShow, KAYIT.GetSON_EL_FULL_HOUSE());
                _txtShow.text = "FULL HOUSE";
                break;
            case TypeOfPokerHand.Flush:
                SetEl(_imgsShow, KAYIT.GetSON_EL_FLUSH());
                _txtShow.text = "FLUSH";
                break;
            case TypeOfPokerHand.Straight:
                SetEl(_imgsShow, KAYIT.GetSSON_EL_STRAIGHTH());
                _txtShow.text = "STRAIGHT";
                break;
            case TypeOfPokerHand.ThreeOfAKind:
                SetEl(_imgsShow, KAYIT.GetSON_EL_THREE_A_KIND());
                _txtShow.text = "THREE OF A KIND";
                break;
            case TypeOfPokerHand.TwoPair:
                SetEl(_imgsShow, KAYIT.GetSON_EL_TWO_PAIR());
                _txtShow.text = "TWO PAIR";
                break;
            case TypeOfPokerHand.JackOrBetter:
            default:
                SetEl(_imgsShow, KAYIT.GetSON_EL_JACK_OR_BETTER());
                _txtShow.text = "JACKS OR BETTER";
                break;
        }
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

    void SetEl(Image[] imgs, List<string> list)
    {
        if (list[0] == "0000")
        {
            for (int i = 0; i < 5; i++)
            {
                imgs[i].sprite = _kartArkasi;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                imgs[i].sprite = kartDestesi.GetKart(list[i]);
            }
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