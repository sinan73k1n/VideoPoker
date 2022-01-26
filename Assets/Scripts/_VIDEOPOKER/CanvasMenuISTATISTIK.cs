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


    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
        AtaTextVar();
    }

    void AtaTextVar()
    {
        _txtRoyalFlus.text = KAYIT.GetWinCountOf_ROYAL_FLUSH().ToString();
        _txtStraightFlush.text = KAYIT.GetWinCountOf_STRAIGHT_FLUSH().ToString();
        _txtFourAKIND.text = KAYIT.GetWinCountOf_FOUR_A_KIND().ToString();
        _txtFullHouse.text = KAYIT.GetWinCountOf_FULL_HOUSE().ToString();
        _txtFlush.text = KAYIT.GetWinCountOf_FLUSH().ToString();
        _txtStraight.text = KAYIT.GetWinCountOf_STRAIGHT().ToString();
        _txtThreeAKIND.text = KAYIT.GetWinCountOf_THREE_A_KIND().ToString();
        _txtTwoPair.text = KAYIT.GetWinCountOf_TWO_PAIR().ToString();
        _txtJackOrBetter.text = KAYIT.GetWinCountOf_JACK_OR_BETTER().ToString();
        _txtGame.text = KAYIT.GetWinCountOf_GAME().ToString();
        _txtMaxCredit.text = KAYIT.GetWinCountOf_MAX_CREDIT().ToString();
        _txtWin.text = KAYIT.GetWinCountOf_WIN().ToString();
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        gameObject.SetActive(false);
    }


    
}