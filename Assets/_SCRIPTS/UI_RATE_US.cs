using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RATE_US : MonoBehaviour
{
    [SerializeField] Button _btnClose, _btnRate;
    [SerializeField] Button[] _btnsYildiz;
    [SerializeField] Image[] _imgsYildiz;


    int _seciliYildiz = -1;
    private void Awake()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KAYIT.SetDegerlendirmeAcildi(true);
        KAYIT.SetDegerlendirmeyiAc(false);
        HandleAta();
    }
    void HandleAta()
    {
        
        for (int i = 0; i < _btnsYildiz.Length; i++)
        {
            int idx = i;
            _btnsYildiz[i].onClick.AddListener(() => HandleBtn(idx));
        }


        _btnClose.onClick.AddListener(HandleClose);   
        _btnRate.onClick.AddListener(HandleRate);
        _btnRate.interactable = false;
        _btnClose.interactable = false;

    }
    void HandleClose()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
    void HandleRate()
    {
        if (_seciliYildiz > 3)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.S7SOFTWARE.VideoPokerJACKORBETTER");
        }
        else
        {

        }
        KAYIT.AddToAnaBakiye(75);
        GameManagerVideoPoker.instance.WriteCreditAndBet();
        HandleClose();
    }
    void HandleBtn(int numara)
    {
        if (_btnRate.interactable == false)
        {
            _btnClose.interactable = true;
            _btnRate.interactable = true;
            foreach (var item in _imgsYildiz)
            {
                item.color = Color.yellow;
            }
        }
        
        _seciliYildiz = numara;
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);

        for (int i = 0; i < _btnsYildiz.Length; i++)
        {
            if (numara >= i)
            {
                _btnsYildiz[i].image.color = Color.yellow;
            }
            else
            {
                _btnsYildiz[i].image.color = Color.red;
            }
           

        }
    }
}
