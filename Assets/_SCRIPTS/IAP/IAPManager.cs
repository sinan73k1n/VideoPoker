using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

/// <summary>
/// Google Play IAP yöneticisi. Sahnede tek bir objeye ekle — DontDestroyOnLoad ile yaşar.
/// Google Play Console'da aşağıdaki Product ID'leri "Consumable" olarak tanımla.
/// </summary>
public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    public static IAPManager instance;

    // ── Product ID'ler — Google Play Console'daki ile birebir eşleşmeli ──
    public const string CREDITS_100  = "credits_100";
    public const string CREDITS_550  = "credits_550";
    public const string CREDITS_1200 = "credits_1200";
    public const string CREDITS_2500 = "credits_2500";

    // Hangi ürün kaç kredi veriyor
    static readonly Dictionary<string, int> _creditAmounts = new()
    {
        { CREDITS_100,  100  },
        { CREDITS_550,  550  },
        { CREDITS_1200, 1200 },
        { CREDITS_2500, 2500 },
    };

    IStoreController    _storeController;
    IExtensionProvider  _extensions;

    public bool IsInitialized => _storeController != null && _extensions != null;

    // ── Başlatma ─────────────────────────────────────────────────────────

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var id in _creditAmounts.Keys)
            builder.AddProduct(id, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    // ── Public API ────────────────────────────────────────────────────────

    /// <summary>Satın alma başlat. Mağaza açılır.</summary>
    public void BuyProduct(string productId)
    {
        if (!IsInitialized)
        {
            Debug.LogWarning("[IAP] Henüz başlatılmadı.");
            return;
        }
        _storeController.InitiatePurchase(productId);
    }

    /// <summary>Yerelleştirilmiş fiyat metni döner (ör: "₺29,99"). Hazır değilse "-" döner.</summary>
    public string GetLocalizedPrice(string productId)
    {
        if (!IsInitialized) return "-";
        Product p = _storeController.products.WithID(productId);
        return p != null ? p.metadata.localizedPriceString : "-";
    }

    // ── IDetailedStoreListener ────────────────────────────────────────────

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _extensions      = extensions;
        Debug.Log("[IAP] Başlatıldı.");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
        => Debug.LogWarning($"[IAP] Başlatma hatası: {error}");

    public void OnInitializeFailed(InitializationFailureReason error, string message)
        => Debug.LogWarning($"[IAP] Başlatma hatası: {error} — {message}");

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        string id = args.purchasedProduct.definition.id;
        if (_creditAmounts.TryGetValue(id, out int amount))
        {
            // Krediyi kaydet
            KAYIT.AddToAnaBakiye(amount);
            // Varsa oyun UI'ını güncelle
            if (GameManagerVideoPoker.instance != null)
                GameManagerVideoPoker.instance.WriteCreditAndBet();
            Debug.Log($"[IAP] Satın alındı: {id} → +{amount} kredi");
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        => Debug.LogWarning($"[IAP] Satın alma başarısız: {product.definition.id} — {failureReason}");

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        => Debug.LogWarning($"[IAP] Satın alma başarısız: {product.definition.id} — {failureDescription.message}");
}
