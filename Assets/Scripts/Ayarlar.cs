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
    void Awake() { _goUyari.SetActive(false); }
    void Start()
    {

        _btnClose.onClick.AddListener(() => HandleExit());
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
    void HandleSes()
    {
        float sesSeviyesi = KAYIT.GetSesSeviyesi();
        if (sesSeviyesi > 1f) { sesSeviyesi = 0.75f; _imgSes.sprite = _spts[0]; }
        else if (sesSeviyesi == 0.75f) { sesSeviyesi = 0.50f; _imgSes.sprite = _spts[1]; }
        else if (sesSeviyesi == 0.50f) { sesSeviyesi = 0.25f; _imgSes.sprite = _spts[2]; }
        else if (sesSeviyesi == 0.25f) { sesSeviyesi=0f; _imgSes.sprite = _spts[3]; }
        else if (sesSeviyesi == 0) { sesSeviyesi = 1f; _imgSes.sprite = _spts[0]; }
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