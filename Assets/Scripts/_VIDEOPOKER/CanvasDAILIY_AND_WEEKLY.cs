using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CanvasDAILIY_AND_WEEKLY : MonoBehaviour
{
    [SerializeField] Button _btnClose, _btnPrev, _btnNext;
    [SerializeField] Button[] _btnGorev;
    [SerializeField] Image[] _imgGorev;
    [SerializeField] TMP_Text[] _txtGorevSayi,_txtGorevAdi;
    [SerializeField] TMP_Text _txtGorevHeader;


int _sayfaNumarasi;
    void Start()
    {
        _sayfaNumarasi=KAYIT.GetSayfaNumarasi_DAILY_AND_WEEK();
        _btnClose.onClick.AddListener(() => HandleExit());
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }


    void ChangePage(bool ileri)
    {

    }
}