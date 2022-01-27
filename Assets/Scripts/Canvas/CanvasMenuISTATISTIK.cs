using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CanvasMenuISTATISTIK : MonoBehaviour
{

    [SerializeField]
    TMP_Text _txtRoyalFlus, _txtStraightFlush, _txtFourAKIND, _txtFullHouse,
     _txtFlush, _txtStraight, _txtThreeAKIND, _txtTwoPair, _txtJackOrBetter, _txtGame, _txtMaxCredit, _txtWin;
    [SerializeField] Button _btnClose;
    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 20;
    }

    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
        AtaTextVar();
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