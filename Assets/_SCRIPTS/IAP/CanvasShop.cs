using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Kredi paketi satın alma paneli.
/// Inspector'da 4 adet Button ve 2'şer TMP_Text (kredi + fiyat) ata.
/// </summary>
public class CanvasShop : MonoBehaviour
{
    [Header("Butonlar")]
    [SerializeField] Button _btnClose;
    [SerializeField] Button _btn100;
    [SerializeField] Button _btn550;
    [SerializeField] Button _btn1200;
    [SerializeField] Button _btn2500;

    [Header("Fiyat Metinleri (her butonun içinde otomatik aranır)")]
    [SerializeField] TMP_Text _txtPrice100;
    [SerializeField] TMP_Text _txtPrice550;
    [SerializeField] TMP_Text _txtPrice1200;
    [SerializeField] TMP_Text _txtPrice2500;

    void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 15;
    }

    void Start()
    {
        _btnClose.onClick.AddListener(HandleClose);
        _btn100 .onClick.AddListener(() => HandleBuy(IAPManager.CREDITS_100));
        _btn550 .onClick.AddListener(() => HandleBuy(IAPManager.CREDITS_550));
        _btn1200.onClick.AddListener(() => HandleBuy(IAPManager.CREDITS_1200));
        _btn2500.onClick.AddListener(() => HandleBuy(IAPManager.CREDITS_2500));

        _txtPrice100.text = "BUY 100\n";
        _txtPrice550.text = "BUY 550\n";
        _txtPrice1200.text = "BUY 1200\n";
        _txtPrice2500.text = "BUY 2500\n";

        RefreshPrices();
    }

    // IAP başlatıldıktan sonra Google Play'den gelen yerelleştirilmiş fiyatları yazar.
    void RefreshPrices()
    {
        if (IAPManager.instance == null || !IAPManager.instance.IsInitialized) return;
        SetPrice(_txtPrice100,  IAPManager.CREDITS_100);
        SetPrice(_txtPrice550,  IAPManager.CREDITS_550);
        SetPrice(_txtPrice1200, IAPManager.CREDITS_1200);
        SetPrice(_txtPrice2500, IAPManager.CREDITS_2500);
    }

    void SetPrice(TMP_Text txt, string productId)
    {
        if (txt == null) return;
        txt.text += IAPManager.instance.GetLocalizedPrice(productId);
    }

    void HandleBuy(string productId)
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        if (IAPManager.instance != null)
            IAPManager.instance.BuyProduct(productId);
        else
            Debug.LogWarning("[Shop] IAPManager bulunamadı.");
    }

    void HandleClose()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
}
