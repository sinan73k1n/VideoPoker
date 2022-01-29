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
    [SerializeField] TMP_Text[] _txtGorevSayi, _txtGorevAdi;
    [SerializeField] TMP_Text _txtGorevHeader;

    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 10;
    }
    int _sayfaNumarasi;
    void Start()
    {
        _sayfaNumarasi = KAYIT.GetSayfaNumarasi_DAILY_AND_WEEK();
        _btnClose.onClick.AddListener(() => HandleExit());
        _btnNext.onClick.AddListener(() => ChangePage(true, _sayfaNumarasi));
        _btnPrev.onClick.AddListener(() => ChangePage(false, _sayfaNumarasi));
    }

    void HandleExit()
    {
        AdControl.instance.CloseBanner();
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }


    void ChangePage(bool ileri, int numaraSayfa)
    {

        if (ileri)
        {
            numaraSayfa++;
            if (numaraSayfa > 5) numaraSayfa = 0;
        }
        else
        {
            numaraSayfa--;
            if (numaraSayfa < 0) numaraSayfa = 5;
        }
        KAYIT.SetSayfaNumarasi_DAILY_AND_WEEK(numaraSayfa);

        switch (numaraSayfa)
        {
            case 0:
                _txtGorevHeader.text = "BET 1$";
                break;
            case 1:
                _txtGorevHeader.text = "BET 2$";
                break;
            case 2:
                _txtGorevHeader.text = "BET 3$";
                break;
            case 3:
                _txtGorevHeader.text = "BET 4$";
                break;
            case 4:
                _txtGorevHeader.text = "BET 5$";
                break;
            case 5:
                _txtGorevHeader.text = "WEEK";
                break;
            default:
                Debug.Log("Hata Sayfa Değiştirme");
                break;
        }

    }
}