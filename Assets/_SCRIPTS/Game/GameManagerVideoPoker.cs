using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerVideoPoker : MonoBehaviour
{
    public static GameManagerVideoPoker instance;

    [SerializeField] KartVideoPoker[] _karts;
    [SerializeField] KartDestesi _desteKartOrg;
    [SerializeField] GameObject _isikKazanc;
    [SerializeField] GameObject _isikKazanTablo;
    [SerializeField] Vector3[] _trfIsikKazanc;
    [SerializeField] Vector3[] _trfIsikKazancTablo;
    [SerializeField] GameObject _sptRenSonuc;
    [SerializeField] GameObject _sptRenGameOver;
    [SerializeField] Button _btnDeal, _btnBetOne, _btnReklam, _btnMenu, _btnGorev,_btnKartlar;
    [SerializeField] TMP_Text _txtWin, _txtBet, _txtCredits, _txtNameOfKazanc, _txtBtnDealDrew;
    [SerializeField] string[] _nameOfKazanc;

   public bool isAdim2 = false;

    List<int> _kartSayisi = new List<int>();


    [SerializeField] float _sureKartAcilma = 0.2f;

    [Header("UI")] [SerializeField] GameObject _goUI_MENU;
    [SerializeField] GameObject _goUI_GOREV, _goUI_CREDITS,_goUI_KARTLAR;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _isikKazanc.SetActive(false);
        _sptRenSonuc.SetActive(false);
        _txtWin.text = "WIN " + 0;

        _txtBtnDealDrew.text = "DEAL";
        WriteCreditAndBet();

        SetActiveCardHold(false);
        CloseAllHold();
        SetSeciliBahisOnTable(KAYIT.GetSeciliBahis());
        CheckBetAndCredit();
        _btnMenu.onClick.AddListener(() => HandleOpenUI(_goUI_MENU));
        _btnBetOne.onClick.AddListener(() => HandleBetOne());
        _btnReklam.onClick.AddListener(() => HandleReklam());
        _btnGorev.onClick.AddListener(() => HandleOpenUI(_goUI_GOREV));
        _btnKartlar.onClick.AddListener(() => HandleOpenUI(_goUI_KARTLAR));
        AtaKartArkasi();
        _btnDeal.onClick.AddListener(HandleOyna);
    }

    void HandleReklam()
    {
        Instantiate(_goUI_CREDITS);
        AdControl.instance.ShowBanner();
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);

    }

    void WriteCreditAndBet()
    {
        _txtBet.text = "" + KAYIT.GetSeciliBahis() + "$";
        _txtCredits.text = "" + KAYIT.GetAnaBakiye();
    }
    void HandleBetOne()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        KAYIT.SetSeciliBahisiBirArttir();
        int bahis = KAYIT.GetSeciliBahis();
        SetSeciliBahisOnTable(bahis);
        _txtBet.text = "" + bahis + "$";

        CheckBetAndCredit();
    }

  public  void CheckBetAndCredit()
    {
        if (KAYIT.GetSeciliBahis() <= KAYIT.GetAnaBakiye()) { _btnDeal.interactable = true; }
        else { _btnDeal.interactable = false; }
    }
    private void HandleOyna()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        StartGame();
    }
    void HandleOpenUI(GameObject gameObject)
    {
        AdControl.instance.ShowBanner();
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Instantiate(gameObject);

    }

    void StartGame()
    {
        if (!isAdim2)
        {
    

            KAYIT_GOREV_YONETICISI.AddOneCountGorev(8, KAYIT.GetSeciliBahis() - 1);

            _txtBtnDealDrew.text = "DRAW";
            _btnBetOne.interactable = false;
            _sptRenGameOver.SetActive(false);
            KAYIT.AddToAnaBakiye(-KAYIT.GetSeciliBahis());
            _txtCredits.text = "" + KAYIT.GetAnaBakiye();
            CloseCards();
            DesteOlustur();
            KartDagit();
            KAYIT.AddOneMore_GAME();


        }
        else
        {
            _txtBtnDealDrew.text = "DEAL";

            CloseCards();
            KartDagit();



        }

    }

    string GetEl()
    {

        return "" + GetKart(_karts[0]) + GetKart(_karts[1]) + GetKart(_karts[2]) + GetKart(_karts[3]) + GetKart(_karts[4]);
    }

    string GetKart(KartVideoPoker kartVideoPoker)
    {
        string kart = "";
        int index = kartVideoPoker._index;
        switch (kartVideoPoker._kartTur)
        {
            case KartTur.Kupa:
                kart = "KU"; break;
            case KartTur.Maca:
                kart = "MA"; break;
            case KartTur.Karo:
                kart = "KA"; break;
            case KartTur.Sinek:

            default:
                kart = "SI"; break;


        }
        return kart + (index < 10 ? "0" + index : index.ToString());
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
        int bahisMiktar = KAYIT.GetSeciliBahis()-1;
        _isikKazanc.SetActive(false);
        if (isAdim2)
        {
            WriteWin(carpan, KAYIT.GetSeciliBahis());
            if (carpan > 0) {
                KAYIT.AddOneMore_WIN();
            KAYIT_GOREV_YONETICISI.AddOneCountGorev(7, KAYIT.GetSeciliBahis() - 1);
                
            }
        }

        switch (carpan)
        {
            case 250:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_ROYAL_FLUSH(GetEl());
                    CountEl(TypeOfPokerHand.RoyalFlush);
       
                }
                SetIsik(0);
                SetSpriteSonuc(_nameOfKazanc[0]); break;
            case 50:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_STRAIGHT_FLUSH(GetEl());
                    CountEl(TypeOfPokerHand.StraightFlush);
                }
                SetIsik(1);
                SetSpriteSonuc(_nameOfKazanc[1]);
                break;
            case 25:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_FOUR_A_KIND(GetEl());
                    CountEl(TypeOfPokerHand.FourOfAKind);
                 
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(0, bahisMiktar);
                }
                SetIsik(2);
                SetSpriteSonuc(_nameOfKazanc[2]);
                break;
            case 9:
                SetIsik(3);
                SetSpriteSonuc(_nameOfKazanc[3]);
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_FULL_HOUSE(GetEl());
                    CountEl(TypeOfPokerHand.FullHouse);
                
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(1, bahisMiktar);


                }
                break;
            case 6:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_FLUSH(GetEl());
                    CountEl(TypeOfPokerHand.Flush);
      
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(2, bahisMiktar);


                }
                SetIsik(4);
                SetSpriteSonuc(_nameOfKazanc[4]);
                break;
            case 4:
                SetIsik(5);
                SetSpriteSonuc(_nameOfKazanc[5]);
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_STRAIGHT(GetEl());
                    CountEl(TypeOfPokerHand.Straight);

                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(3, bahisMiktar);


                }
                break;
            case 3:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_THREE_A_KIND(GetEl());
                    CountEl(TypeOfPokerHand.ThreeOfAKind);
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(4, bahisMiktar);


                }
                SetIsik(6);
                SetSpriteSonuc(_nameOfKazanc[6]);
                break;
            case 2:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_TWO_PAIR(GetEl());
                    CountEl(TypeOfPokerHand.TwoPair);
              
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(5, bahisMiktar);


                }
                SetIsik(7);
                SetSpriteSonuc(_nameOfKazanc[7]);
                break;
            case 1:
                if (isAdim2)
                {
                    KAYIT.SetSON_EL_JACK_OR_BETTER(GetEl());
      
                    KAYIT_GOREV_YONETICISI.AddOneCountGorev(6, bahisMiktar);


                }
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
    public void AddCredits(int credits)
    {
        KAYIT.AddToAnaBakiye(credits);
        _txtCredits.text = "" + KAYIT.GetAnaBakiye();
        
        if (isAdim2)
        {
            if (KAYIT.GetSeciliBahis() <= KAYIT.GetAnaBakiye()) { _btnDeal.interactable = true; }
            else { _btnDeal.interactable = false; }
        }

    }
    void WriteWin(int carpan, int bahis)
    {
        int kazanc = bahis * carpan;
        if (kazanc == 1250) kazanc = 4000;

        KAYIT.AddToAnaBakiye(kazanc);
        _txtWin.text = "WIN " + kazanc;
        _txtCredits.text = "" + KAYIT.GetAnaBakiye();
    }

    void SetSeciliBahisOnTable(int bahis)
    {
        _isikKazanTablo.transform.localPosition = _trfIsikKazancTablo[bahis - 1];

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
    void CountEl(TypeOfPokerHand type)
    {
        { KAYIT.AddOneHand(type); }
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
        if (!isAdim2) _btnBetOne.interactable = true;
        if (KAYIT.GetAnaBakiye() >= KAYIT.GetSeciliBahis() || isAdim2) _btnDeal.interactable = true;

    }



    void CloseCards()
    {
        if (!_karts[0]._isHolding) _karts[0].CloseCard();
        if (!_karts[1]._isHolding) _karts[1].CloseCard();
        if (!_karts[2]._isHolding) _karts[2].CloseCard();
        if (!_karts[3]._isHolding) _karts[3].CloseCard();
        if (!_karts[4]._isHolding) _karts[4].CloseCard();
    }
  public  void AtaKartArkasi()
    {
        Sprite kartArkasi = _desteKartOrg.GetBackOfCard(KART_SATIS.GetAktifKart());
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
            if (_karts[1]._index == _karts[i]._index) sayi++;
        }
        if (sayi == 4) return true; else return false;
    }
    bool FullHouse()
    {
        bool have3Card = false;
        bool have2Card = false;
        int indexFirstCard = 0;
        int sayi = 1;
        for (int i = 0; i < 3; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 3)
            {
                indexFirstCard = _karts[i]._index;
                have3Card = true;
                break;
            }
            sayi = 1;
        }
        if (!have3Card) return false;
        sayi = 1;
        for (int i = 0; i < 5; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == indexFirstCard) break;
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 2)
            {
                have2Card = true;
                break;
            }
            sayi = 1;
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

        int sayi = 1;
        for (int i = 0; i < 3; i++)
        {
            for (var j = i + 1; j < 5; j++)
            {
                if (_karts[i]._index == _karts[j]._index) sayi++;
            }
            if (sayi == 3)
            {

                return true;
            }
            sayi = 1;
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