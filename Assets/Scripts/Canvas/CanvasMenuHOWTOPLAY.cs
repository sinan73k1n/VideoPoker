using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasMenuHOWTOPLAY : MonoBehaviour
{
    [SerializeField] Button _btnClose,_btnGeri,_btnIleri;
    [SerializeField] GameObject _1, _2, _3, _4, _5, _6, _7, _8, _9;
    [SerializeField] GameObject[] _sayfaNoktalari;
    private void Awake()
    {
        Canvas canvas =
        GetComponent<Canvas>();
        canvas.sortingOrder = 20;
        canvas.worldCamera = Camera.main;
    }
    int _sayfa = 0;
    void Start()
    {

        SayfaDegistir(_sayfa);
        _btnGeri.interactable = false;
        _btnClose.onClick.AddListener(() => HandleExit());
        _btnGeri.onClick.AddListener(() => HandleGeri());
        _btnIleri.onClick.AddListener(() => HandleIleri());
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
    void HandleIleri()
    {
        _sayfa++;
       
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        SayfaDegistir(_sayfa);
    }
    void HandleGeri()
    {
        _sayfa--;
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        SayfaDegistir(_sayfa);
    }



    void SayfaDegistir(int hangi)
    {
        switch (hangi)
        {
            case 1:
                _1.SetActive(false); _2.SetActive(false); _3.SetActive(false);
                _4.SetActive(true); _5.SetActive(true); _6.SetActive(true);
                _7.SetActive(false); _8.SetActive(false); _9.SetActive(false);
                _btnGeri.interactable = true;_btnIleri.interactable = true;
                _sayfaNoktalari[0].SetActive(false);
                _sayfaNoktalari[1].SetActive(true);
                _sayfaNoktalari[2].SetActive(false);
                break;
            case 2:
                _1.SetActive(false); _2.SetActive(false); _3.SetActive(false);
                _4.SetActive(false); _5.SetActive(false); _6.SetActive(false);
                _7.SetActive(true); _8.SetActive(true); _9.SetActive(true);
                _btnGeri.interactable = true; _btnIleri.interactable = false;
                _sayfaNoktalari[0].SetActive(false);
                _sayfaNoktalari[1].SetActive(false);
                _sayfaNoktalari[2].SetActive(true);

                break;
            case 0:
            default:
                _1.SetActive(true); _2.SetActive(true); _3.SetActive(true);
                _4.SetActive(false); _5.SetActive(false); _6.SetActive(false);
                _7.SetActive(false); _8.SetActive(false); _9.SetActive(false);
                _btnGeri.interactable = false; _btnIleri.interactable = true;
                _sayfaNoktalari[0].SetActive(true);
                _sayfaNoktalari[1].SetActive(false);
                _sayfaNoktalari[2].SetActive(false);

                break;
        }
    }
}