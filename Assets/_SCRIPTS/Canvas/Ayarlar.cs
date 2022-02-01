using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Ayarlar : MonoBehaviour
{
    [SerializeField] Button _btnClose, _btnSes, _btnSil, _btnEvet, _btnHayir;
    [SerializeField] GameObject _goUyari;
    [SerializeField] Image _imgSes;
    [SerializeField] Sprite[] _spts;

    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 20;
        _goUyari.SetActive(false);
    }
  
    void Start()
    {
        Ayarla_btnSes();
        AtaHandleToButtons();
   
    }

    void Ayarla_btnSes()
    {
        float sesSeviyesi = KAYIT.GetSesSeviyesi();
        if (sesSeviyesi == 1f) { _imgSes.sprite = _spts[3]; }
        else if (sesSeviyesi == 0.65f) { _imgSes.sprite = _spts[2]; }
        else if (sesSeviyesi == 0.35f) { _imgSes.sprite = _spts[1]; }
        else if (sesSeviyesi == 0) { _imgSes.sprite = _spts[0]; }
    }
    void AtaHandleToButtons()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
        _btnSes.onClick.AddListener(() => HandleSes());
        _btnSil.onClick.AddListener(() => HandleDelete());
        _btnHayir.onClick.AddListener(() => HandleHayir());
        _btnEvet.onClick.AddListener(() => StartCoroutine(HandleEvet()));
    }
    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
    void HandleSes()
    {
        float sesSeviyesi = KAYIT.GetSesSeviyesi();
        Debug.Log(sesSeviyesi);
        if (sesSeviyesi == 1f) { sesSeviyesi = 0.65f; _imgSes.sprite = _spts[2]; }
        else if (sesSeviyesi == 0.65f) { sesSeviyesi = 0.35f; _imgSes.sprite = _spts[1]; }
        else if (sesSeviyesi == 0.35f) { sesSeviyesi=0f; _imgSes.sprite = _spts[0]; }
        else if (sesSeviyesi == 0) { sesSeviyesi = 1f; _imgSes.sprite = _spts[3]; }
        SesKutusu.instance.SetVolume(sesSeviyesi);
        KAYIT.SetSesSeviyesi(sesSeviyesi);
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);

    }

    void HandleDelete()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _goUyari.SetActive(true);
    }
    IEnumerator HandleEvet()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        PlayerPrefs.DeleteAll();
    
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    void HandleHayir()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        _goUyari.SetActive(false);
    }

}