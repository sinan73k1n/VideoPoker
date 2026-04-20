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
    [SerializeField] IntVariable _credits;
    [SerializeField] GameObject _isikKazanc;
    [SerializeField] GameObject _isikKazanTablo;
    [SerializeField] Vector3[] _trfIsikKazanc;
    [SerializeField] Vector3[] _trfIsikKazancTablo;
    [SerializeField] GameObject _sptRenSonuc;
    [SerializeField] GameObject _sptRenGameOver;
    [SerializeField] Button _btnDeal, _btnBetOne, _btnReklam, _btnMenu, _btnGorev, _btnKartlar;
    [SerializeField] TMP_Text _txtWin, _txtBet, _txtCredits, _txtNameOfKazanc, _txtBtnDealDrew, _txtBtnGorev;
    [SerializeField] string[] _nameOfKazanc;

    public bool isAdim2 = false;

    List<int> _kartSayisi = new List<int>();

    [SerializeField] float _sureKartAcilma = 0.2f;

    [Header("UI")] [SerializeField] GameObject _goUI_MENU;
    [SerializeField] GameObject _goUI_GOREV, _goUI_CREDITS, _goUI_KARTLAR, _goUI_RATE_US;

    float _counterADS;
    bool _readtForADS = false;

    private void Awake()
    {
        instance = this;
        _counterADS = KAYIT.GetAdsCountTime();
        if (_credits != null)
        {
            _credits.Value = KAYIT.GetAnaBakiye();
            _credits.OnValueChanged += v => _txtCredits.text = v.ToString();
        }
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
        ShowRewardCount();
    }

    private void Update()
    {
        CounterADS();
    }

    private void CounterADS()
    {
        if (!KAYIT.GetReklamVar()) return;
        if (_readtForADS) return;
        if (_counterADS < 900) _counterADS += Time.deltaTime;
        else _readtForADS = true;
    }

    void HandleReklam()
    {
        Instantiate(_goUI_CREDITS);
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
    }

    public void WriteCreditAndBet()
    {
        _txtBet.text = "" + KAYIT.GetSeciliBahis() + "$";
        SetCredits(KAYIT.GetAnaBakiye());
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

    public void CheckBetAndCredit()
    {
        _btnDeal.interactable = KAYIT.GetSeciliBahis() <= KAYIT.GetAnaBakiye();
    }

    private void OnApplicationQuit()
    {
        if (!KAYIT.GetReklamVar()) return;
        KAYIT.SetAdsCountTime(_counterADS);
    }

    private void HandleOyna()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        StartGame();
        if (_readtForADS)
        {
            _readtForADS = false;
            _counterADS = 0;
            KAYIT.SetAdsCountTime(_counterADS);
        }
    }

    void HandleOpenUI(GameObject gameObject)
    {
        GOREV_YONETICISI.instance._isOpenTable = true;
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Instantiate(gameObject);
    }

    void RATE()
    {
        if (KAYIT.GetDegerlendirmeAcildi()) return;
        KAYIT.SetDegerlendirmeKalanSayi();
        if (KAYIT.GetDegerlendirmeyiAc()) Instantiate(_goUI_RATE_US);
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
            SetCredits(KAYIT.GetAnaBakiye());
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

    void SetActiveCardHold(bool deger)
    {
        foreach (var item in _karts)
            item.SetActiveHold(deger);
    }

    void CloseAllHold()
    {
        foreach (var item in _karts)
            item.SetHold(false);
    }

    void KazancYeriGoster(int carpan)
    {
        int bahisMiktar = KAYIT.GetSeciliBahis() - 1;
        _isikKazanc.SetActive(false);

        if (isAdim2)
        {
            WriteWin(carpan, KAYIT.GetSeciliBahis());
            if (carpan > 0)
            {
                KAYIT.AddOneMore_WIN();
                KAYIT_GOREV_YONETICISI.AddOneCountGorev(7, KAYIT.GetSeciliBahis() - 1);
            }
        }

        switch (carpan)
        {
            case 250:
                if (isAdim2) { KAYIT.SetSON_EL_ROYAL_FLUSH(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.RoyalFlush); }
                SetIsik(0); SetSpriteSonuc(_nameOfKazanc[0]); break;
            case 50:
                if (isAdim2) { KAYIT.SetSON_EL_STRAIGHT_FLUSH(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.StraightFlush); }
                SetIsik(1); SetSpriteSonuc(_nameOfKazanc[1]); break;
            case 25:
                if (isAdim2) { KAYIT.SetSON_EL_FOUR_A_KIND(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.FourOfAKind); KAYIT_GOREV_YONETICISI.AddOneCountGorev(0, bahisMiktar); }
                SetIsik(2); SetSpriteSonuc(_nameOfKazanc[2]); break;
            case 9:
                if (isAdim2) { KAYIT.SetSON_EL_FULL_HOUSE(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.FullHouse); KAYIT_GOREV_YONETICISI.AddOneCountGorev(1, bahisMiktar); }
                SetIsik(3); SetSpriteSonuc(_nameOfKazanc[3]); break;
            case 6:
                if (isAdim2) { KAYIT.SetSON_EL_FLUSH(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.Flush); KAYIT_GOREV_YONETICISI.AddOneCountGorev(2, bahisMiktar); }
                SetIsik(4); SetSpriteSonuc(_nameOfKazanc[4]); break;
            case 4:
                if (isAdim2) { KAYIT.SetSON_EL_STRAIGHT(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.Straight); KAYIT_GOREV_YONETICISI.AddOneCountGorev(3, bahisMiktar); }
                SetIsik(5); SetSpriteSonuc(_nameOfKazanc[5]); break;
            case 3:
                if (isAdim2) { KAYIT.SetSON_EL_THREE_A_KIND(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.ThreeOfAKind); KAYIT_GOREV_YONETICISI.AddOneCountGorev(4, bahisMiktar); }
                SetIsik(6); SetSpriteSonuc(_nameOfKazanc[6]); break;
            case 2:
                if (isAdim2) { KAYIT.SetSON_EL_TWO_PAIR(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.TwoPair); KAYIT_GOREV_YONETICISI.AddOneCountGorev(5, bahisMiktar); }
                SetIsik(7); SetSpriteSonuc(_nameOfKazanc[7]); break;
            case 1:
                if (isAdim2) { KAYIT.SetSON_EL_JACK_OR_BETTER(PokerHandEvaluator.GetEl(_karts)); CountEl(TypeOfPokerHand.JackOrBetter); KAYIT_GOREV_YONETICISI.AddOneCountGorev(6, bahisMiktar); }
                SetIsik(8); SetSpriteSonuc(_nameOfKazanc[8]); break;
            default:
                _isikKazanc.SetActive(false); SetSpriteSonuc(""); break;
        }

        if (isAdim2) ShowRewardCount();
    }

    void SetIsik(int siraTransform)
    {
        _isikKazanc.transform.position = _trfIsikKazanc[siraTransform];
        _isikKazanc.SetActive(true);
        SesKutusu.instance.Play(NameOfAudioClip.Coin1);
    }

    // Tüm kredi değişiklikleri bu metod üzerinden geçer
    void SetCredits(int newValue)
    {
        if (_credits != null) _credits.Value = newValue;
        else _txtCredits.text = newValue.ToString();
    }

    public void AddCredits(int amount)
    {
        KAYIT.AddToAnaBakiye(amount);
        SetCredits(KAYIT.GetAnaBakiye());
        if (isAdim2)
            _btnDeal.interactable = KAYIT.GetSeciliBahis() <= KAYIT.GetAnaBakiye();
    }

    void WriteWin(int carpan, int bahis)
    {
        int kazanc = bahis * carpan;
        if (kazanc == 1250) kazanc = 4000;
        KAYIT.AddToAnaBakiye(kazanc);
        _txtWin.text = "WIN " + kazanc;
        SetCredits(KAYIT.GetAnaBakiye());
    }

    void SetSeciliBahisOnTable(int bahis)
    {
        _isikKazanTablo.transform.localPosition = _trfIsikKazancTablo[bahis - 1];
    }

    void SetSpriteSonuc(string textSonuc)
    {
        if (!isAdim2) { _sptRenSonuc.SetActive(false); return; }
        _txtNameOfKazanc.text = textSonuc;
        _sptRenSonuc.SetActive(true);
    }

    void CountEl(TypeOfPokerHand type)
    {
        KAYIT.AddOneHand(type);
    }

    void DesteOlustur()
    {
        _kartSayisi.Clear();
        for (int i = 0; i < 52; i++)
            _kartSayisi.Add(i);
    }

    void KartDagit()
    {
        StartCoroutine(crKartDagit());
    }

    KartTur GetKartTur(int index)
    {
        if (index <= 12) return KartTur.Sinek;
        if (index <= 25) return KartTur.Maca;
        if (index <= 38) return KartTur.Kupa;
        return KartTur.Karo;
    }

    int GetKartIndex(int index)
    {
        if (index < 13) return index;
        if (index < 26) return index - 13;
        if (index < 39) return index - 26;
        return index - 39;
    }

    IEnumerator crKartDagit()
    {
        _btnDeal.interactable = false;
        SetActiveCardHold(false);
        yield return new WaitForSeconds(_sureKartAcilma);

        for (int i = 0; i < _karts.Length; i++)
        {
            if (!_karts[i]._isHolding)
            {
                SesKutusu.instance.Play(NameOfAudioClip.VideoPokerKartDagit);
                int index = _kartSayisi[Random.Range(0, _kartSayisi.Count)];
                _kartSayisi.Remove(index);
                KartTur kartTur = GetKartTur(index);
                index = GetKartIndex(index);
                _karts[i].SetKart(_desteKartOrg.GetKart(kartTur, index), kartTur, index + 1);
                _karts[i].OpenCard();
                yield return new WaitForSeconds(_sureKartAcilma);
            }
        }

        KazancYeriGoster(PokerHandEvaluator.Evaluate(_karts));
        SetActiveCardHold(true);

        if (isAdim2)
        {
            _sptRenGameOver.SetActive(true);
            RATE();
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
        foreach (var item in _karts)
            if (!item._isHolding) item.CloseCard();
    }

    public void ShowRewardCount()
    {
        _txtBtnGorev.text = GetAktifOdulSayisi.Hangi(true).ToString();
    }

    public void AtaKartArkasi()
    {
        Sprite kartArkasi = _desteKartOrg.GetBackOfCard(KART_SATIS.GetAktifKart());
        foreach (var item in _karts)
            item.SetKartArka(kartArkasi);
    }
}
