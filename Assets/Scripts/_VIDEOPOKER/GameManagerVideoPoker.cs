using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerVideoPoker : MonoBehaviour
{

    [SerializeField] KartVideoPoker[] _karts;
    [SerializeField] KartDestesi _desteKartOrg;
    [SerializeField] GameObject _isikKazanc;
    [SerializeField] Vector3[] _trfIsikKazanc;
    [SerializeField] Vector3[] _trfIsikKazancTablo;
    [SerializeField] GameObject _sptRenSonuc;
    [SerializeField] GameObject _sptRenGameOver;
    [SerializeField] Button _btnDeal, _btnBetOne, btnReklam, btnMenu;
    [SerializeField] TMP_Text _txtWin, _txtBet, _txtCredits, _txtNameOfKazanc,_txtBtnDealDrew;
    [SerializeField] string[] _nameOfKazanc;


    bool isAdim2 = false;
    float _bahis = 1, _anaPara = 50;
    List<int> _kartSayisi = new List<int>();


    [SerializeField] float _sureKartAcilma = 0.2f;

    private void Start()
    {
        _isikKazanc.SetActive(false);
        _sptRenSonuc.SetActive(false);
        _txtWin.text = "WIN " + 0;
        _txtBet.text = "" + _bahis;
        _txtBtnDealDrew.text = "DEAL";
        //_txtCredits.text = AnaPara.GetAnaPara().ToString();
        _txtCredits.text =""+ _anaPara;
        SetActiveCardHold(false);
        CloseAllHold();
        WriteTablo(((int)_bahis));
        btnMenu.onClick.AddListener(() =>StartCoroutine( HandleAnaMenu())) ;
        AtaKartArkasi();
        _btnDeal.onClick.AddListener(HandleOyna);
    }

    private void HandleOyna()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        StartGame();
    }
    IEnumerator HandleAnaMenu()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        yield return new WaitForSeconds(0.6f);
  
    }

    void StartGame()
    {
        if (!isAdim2)
        {
          
            _txtBtnDealDrew.text = "DREW";

            _sptRenGameOver.SetActive(false);
            _anaPara -= _bahis;
            _txtCredits.text = "" + _anaPara;
            CloseCards();
            DesteOlustur();
            KartDagit();
         


        }
        else
        {
            _txtBtnDealDrew.text = "DEAL";

            CloseCards();
            KartDagit();



        }

    }

    void SetActiveCardHold(bool deger)
    {
        foreach (var item in _karts)
        {
            item.SetActiveHold(deger);
        }
    }

    void CloseAllHold()
    {
        _karts[0].SetHold(false); _karts[1].SetHold(false); _karts[2].SetHold(false); _karts[3].SetHold(false); _karts[4].SetHold(false);

    }
    void KazancYeriGoster(int carpan)
    {
        _isikKazanc.SetActive(false);
        if (isAdim2) WriteWin(carpan, 1);
        switch (carpan)
        {
            case 250:
                SetIsik(0);
                SetSpriteSonuc(_nameOfKazanc[0]);
                break;
            case 50:
                SetIsik(1);
                SetSpriteSonuc(_nameOfKazanc[1]);
                break;
            case 25:
                SetIsik(2);
                SetSpriteSonuc(_nameOfKazanc[2]);
                break;
            case 9:
                SetIsik(3);
                SetSpriteSonuc(_nameOfKazanc[3]);
                break;
            case 6:
                SetIsik(4);
                SetSpriteSonuc(_nameOfKazanc[4]);
                break;
            case 4:
                SetIsik(5);
                SetSpriteSonuc(_nameOfKazanc[5]);
                break;
            case 3:
                SetIsik(6);
                SetSpriteSonuc(_nameOfKazanc[6]);
                break;
            case 2:
                SetIsik(7);
                SetSpriteSonuc(_nameOfKazanc[7]);
                break;
            case 1:
                SetIsik(8);
                SetSpriteSonuc(_nameOfKazanc[8]);
                break;

            default:
                _isikKazanc.SetActive(false);
                SetSpriteSonuc("");
                break;
        }


    }
    void SetIsik(int siraTransform)
    {
        _isikKazanc.transform.position = _trfIsikKazanc[siraTransform];
        _isikKazanc.SetActive(true);
        SesKutusu.instance.Play(NameOfAudioClip.Coin1);

    }

    void WriteWin(int carpan,int bahis)
    {
        int kazanc = bahis * carpan;
        _anaPara += kazanc;
        _txtWin.text = "WIN " + kazanc;
        _txtCredits.text = "" + _anaPara;
    }

    void WriteTablo(int bahis)
    {
    }
    void SetSpriteSonuc(string textSonuc)
    {
        if (!isAdim2)
        {
            _sptRenSonuc.SetActive(false);
        }
        else
        {
            _txtNameOfKazanc.text = textSonuc;
            _sptRenSonuc.SetActive(true);
            
        }
    }
    void DesteOlustur()
    {
        _kartSayisi.Clear();
        for (int i = 0; i < 52; i++)
        {
            _kartSayisi.Add(i);
        }

    }
    void KartDagit()
    {
        StartCoroutine(crKartDagit());
    }

    KartTur GetKartTur(int index)
    {
        if (0 <= index && 12 >= index) return KartTur.Sinek;
        else if (13 <= index && 25 >= index) return KartTur.Maca;
        else if (26 <= index && 38 >= index) return KartTur.Kupa;
        else return KartTur.Karo;
    }
    int GetKartIndex(int index)
    {
        if (13 > index) return index;
        else if (26 > index) return index - 13;
        else if (39 > index) return index - 26;
        else return index - 39;

    }

    IEnumerator crKartDagit()
    {
        _btnDeal.interactable = false;
        SetActiveCardHold(false);
        yield return new WaitForSeconds(_sureKartAcilma);
        if (!_karts[0]._isHolding)
        {
            SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
            int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
            _kartSayisi.Remove(index);
            KartTur kartTur = GetKartTur(index);
            index = GetKartIndex(index);
            _karts[0].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
            _karts[0].OpenCard();
            yield return new WaitForSeconds(_sureKartAcilma);
        }
        if (!_karts[1]._isHolding)
        {
            SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
            int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
            _kartSayisi.Remove(index);
            KartTur kartTur = GetKartTur(index);
            index = GetKartIndex(index);
            _karts[1].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
            _karts[1].OpenCard();
            yield return new WaitForSeconds(_sureKartAcilma);
        }
        if (!_karts[2]._isHolding)
        {
            SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
            int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
            _kartSayisi.Remove(index);
            KartTur kartTur = GetKartTur(index);
            index = GetKartIndex(index);
            _karts[2].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
            _karts[2].OpenCard();
            yield return new WaitForSeconds(_sureKartAcilma);
        }
        if (!_karts[3]._isHolding)
        {
            SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
            int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
            _kartSayisi.Remove(index);
            KartTur kartTur = GetKartTur(index);
            index = GetKartIndex(index);
            _karts[3].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
            _karts[3].OpenCard();
            yield return new WaitForSeconds(_sureKartAcilma);
        }
        if (!_karts[4]._isHolding)
        {
            SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
            int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
            _kartSayisi.Remove(index);
            KartTur kartTur = GetKartTur(index);
            index = GetKartIndex(index);
            _karts[4].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
            _karts[4].OpenCard();
            yield return new WaitForSeconds(_sureKartAcilma);
        }
        KazancYeriGoster(CheckCardsKazanc());


        SetActiveCardHold(true);

        if (isAdim2)
        {
            _sptRenGameOver.SetActive(true);
            CloseAllHold();
            SetActiveCardHold(false);
        }
        isAdim2 = isAdim2 ? false : true;
        yield return new WaitForSeconds(_sureKartAcilma);
        if (_anaPara!=0 )_btnDeal.interactable = true;

    }

    void CloseCards()
    {
        if (!_karts[0]._isHolding) _karts[0].CloseCard();
        if (!_karts[1]._isHolding) _karts[1].CloseCard();
        if (!_karts[2]._isHolding) _karts[2].CloseCard();
        if (!_karts[3]._isHolding) _karts[3].CloseCard();
        if (!_karts[4]._isHolding) _karts[4].CloseCard();
    }
    void AtaKartArkasi()
    {
        Sprite kartArkasi = _desteKartOrg.GetBackOfCardRandom();
        foreach (var item in _karts)
        {
            item.SetKartArka(kartArkasi);
        }

    }

    int CheckCardsKazanc()
    {
        if (RoyalFlush()) return 250;
        if (StraightFlush()) return 50;
        if (FourOfAKind()) return 25;
        if (FullHouse()) return 9;
        if (Flush()) return 6;
        if (Straight()) return 4;
        if (ThreeOfKind()) return 3;
        if (TwoPair()) return 2;
        if (JacksOrBetter()) return 1;
        return 0;

    }

    bool RoyalFlush()
    {
        if (!Flush()) return false;
        bool isRoyalFlush0 = false, isRoyalFlush1 = false, isRoyalFlush2 = false, isRoyalFlush3 = false, isRoyalFlush4 = false;
        foreach (var item in _karts)
        {
            if (item._index == 13) isRoyalFlush0 = true;
            if (item._index == 12) isRoyalFlush0 = true;
            if (item._index == 11) isRoyalFlush0 = true;
            if (item._index == 10) isRoyalFlush0 = true;
            if (item._index == 1) isRoyalFlush0 = true;

        }

        if (isRoyalFlush0 && isRoyalFlush1 && isRoyalFlush2 && isRoyalFlush3 && isRoyalFlush4) return true; else return false;


    }
    bool StraightFlush()
    {
        if (!Flush()) return false;
        int kart = 14;
        foreach (var item in _karts)
        {
            if (item._index < kart)
            {
                kart = item._index;
            }
        }

        bool isKart2 = false, isKart3 = false, isKart4 = false, isKart5 = false;
        foreach (var item in _karts)
        {
            if (kart + 1 == item._index) isKart2 = true;
            if (kart + 2 == item._index) isKart3 = true;
            if (kart + 3 == item._index) isKart4 = true;
            if (kart + 4 == item._index) isKart5 = true;
        }
        if (isKart2 && isKart3 && isKart4 && isKart5) return true; else return false;
    }
    bool FourOfAKind()
    {
        int sayi = 1;
        for (var i = 1; i < 5; i++)
        {
            if (_karts[0]._index == _karts[i]._index) sayi++;
        }
        if (sayi == 4) return true;
        sayi = 1;
        for (var i = 2; i < 5; i++)
        {
            if (_karts[0]._index == _karts[i]._index) sayi++;
        }
        if (sayi == 4) return true; else return false;
    }
    bool FullHouse()
    {
        bool have3Card = false;
        bool have2Card = false;
        int sayi = 0;
        for (int i = 0; i < 3; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 3)
            {
                have3Card = true;
                break;
            }
            sayi = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 2)
            {
                have2Card = true;
                break;
            }
            sayi = 0;
        }

        return (have3Card && have2Card) ? true : false;


    }
    bool Flush()
    {
        KartTur hangiTur = _karts[0]._kartTur;
        for (int i = 1; i < 5; i++)
        {
            if (_karts[0]._kartTur != _karts[i]._kartTur)
                return false;

        }
        return true;
    }
    bool Straight()
    {
        bool haveAOne = false;
        foreach (var item in _karts)
        {
            if (item._index == 1)
            {
                haveAOne = true;
                break;
            }
        }
        if (haveAOne)
        {
            int kart = 1;
            bool isKart2 = false, isKart3 = false, isKart4 = false, isKart5 = false;
            foreach (var item in _karts)
            {
                if (kart + 1 == item._index) isKart2 = true;
                if (kart + 2 == item._index) isKart3 = true;
                if (kart + 3 == item._index) isKart4 = true;
                if (kart + 4 == item._index) isKart5 = true;
            }
            if (isKart2 && isKart3 && isKart4 && isKart5) return true;
            else
            {
                kart = 14;
                isKart2 = false; isKart3 = false; isKart4 = false; isKart5 = false;
                foreach (var item in _karts)
                {
                    if (kart - 1 == item._index) isKart2 = true;
                    if (kart - 2 == item._index) isKart3 = true;
                    if (kart - 3 == item._index) isKart4 = true;
                    if (kart - 4 == item._index) isKart5 = true;
                }
                if (isKart2 && isKart3 && isKart4 && isKart5) return true; else return false;
            }


        }
        else
        {
            int kart = 14;
            foreach (var item in _karts)
            {
                if (item._index < kart)
                {
                    kart = item._index;
                }
            }

            bool isKart2 = false, isKart3 = false, isKart4 = false, isKart5 = false;
            foreach (var item in _karts)
            {
                if (kart + 1 == item._index) isKart2 = true;
                if (kart + 2 == item._index) isKart3 = true;
                if (kart + 3 == item._index) isKart4 = true;
                if (kart + 4 == item._index) isKart5 = true;
            }
            if (isKart2 && isKart3 && isKart4 && isKart5) return true; else return false;
        }
    }
    bool ThreeOfKind()
    {

        int sayi = 0;
        for (int i = 0; i < 3; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 2)
            {

                return true;
            }
            sayi = 0;
        }
        return false;
    }
    bool TwoPair()
    {
        bool isFirstPair = false;
        for (int i = 0; i < 5; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index)
                {
                    if (!isFirstPair)
                    {
                        isFirstPair = true;
                        break;
                    }
                    else if (isFirstPair) return true;
                }
            }
        }
        return false;
    }

    bool JacksOrBetter()
    {
        bool haveAJ = false, haveAQ = false, haveAK = false, haveAOne = false;
        foreach (var item in _karts)
        {
            if (item._index == 11) haveAJ = true;
            if (item._index == 12) haveAQ = true;
            if (item._index == 13) haveAK = true;
            if (item._index == 1) haveAOne = true;
        }

        if (haveAJ)
        {
            int j = 0;
            foreach (var item in _karts)
            {
                if (item._index == 11) j++;
            }
            if (j == 2) return true;
        }

        if (haveAQ)
        {
            int j = 0;
            foreach (var item in _karts)
            {
                if (item._index == 12) j++;
            }
            if (j == 2) return true;
        }

        if (haveAK)
        {
            int j = 0;
            foreach (var item in _karts)
            {
                if (item._index == 13) j++;
            }
            if (j == 2) return true;
        }

        if (haveAOne)
        {
            int j = 0;
            foreach (var item in _karts)
            {
                if (item._index == 1) j++;
            }
            if (j == 2) return true;
        }

        return false;

    }
}