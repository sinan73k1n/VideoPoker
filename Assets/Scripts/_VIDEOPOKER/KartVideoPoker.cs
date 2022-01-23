using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KartVideoPoker : MonoBehaviour
{
    public int _index;
    public KartTur _kartTur;
    public bool _isHolding=false;
    [SerializeField] Button _myBtn;
    Sprite _sptOn, _sptArka;
    SpriteRenderer _sptRen;
    [SerializeField] TMP_Text _txtPro;
    void Awake()
    {
        _sptRen = GetComponent<SpriteRenderer>();
        _txtPro.enabled = _isHolding;
        _myBtn.onClick.AddListener(HandleHold);
    }

    private void HandleHold()
    {
        SetHold(!_isHolding);
    }

    public void OpenCard()
    {
        _sptRen.sprite = _sptOn;
    }

    public void CloseCard()
    {
        _sptRen.sprite = _sptArka;
    }
    public void SetHold(bool deger)
    {
        _isHolding = deger;
        _txtPro.enabled = _isHolding;
    }
    public void SetKart(Sprite sptOn, KartTur kartTur, int index)
    {
        _sptOn = sptOn;
        _index = index;
        _kartTur = kartTur;

    }
    public void SetKartArka(Sprite sptArka)
    {
        _sptArka = sptArka;
    }
    public void SetActiveHold(bool deger)
    {
        _myBtn.interactable = deger;
    }
}