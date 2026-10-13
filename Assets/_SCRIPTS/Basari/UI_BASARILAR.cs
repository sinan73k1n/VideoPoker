using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Başarılar penceresi. Yöneticiye bağlı değil: listeyi ve sayaçları kendisi okur,
// böylece yönetici prefabı sahnede olmasa da pencere açılır.
public class UI_BASARILAR : MonoBehaviour
{
    [SerializeField] BASARI_LISTESI   _liste;
    [SerializeField] UI_BASARI_SATIRI _satirPrefab;
    [SerializeField] Transform        _icerik;      // ScrollRect → Content (VerticalLayoutGroup)
    [SerializeField] TMP_Text         _txtOzet;
    [SerializeField] Button           _btnClose;

    void Awake()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas != null) canvas.sortingOrder = 20;
    }

    void Start()
    {
        if (_btnClose != null) _btnClose.onClick.AddListener(HandleClose);
        BASARI_YONETICISI.BasariAcildi += HandleBasariAcildi;
        Doldur();
    }

    void OnDestroy()
    {
        BASARI_YONETICISI.BasariAcildi -= HandleBasariAcildi;
    }

    void HandleBasariAcildi(BASARI_TANIM b) => Doldur();

    void Doldur()
    {
        if (_liste == null || _satirPrefab == null || _icerik == null) return;

        foreach (Transform eski in _icerik) Destroy(eski.gameObject);

        int acik = 0, puan = 0;
        foreach (var b in _liste.basarilar)
        {
            if (b == null || string.IsNullOrEmpty(b.id)) continue;
            bool acikMi = KAYIT_BASARI.GetAcik(b.id);
            if (acikMi) { acik++; puan += b.puan; }
            Instantiate(_satirPrefab, _icerik).Ata(b, acikMi, KAYIT_BASARI.GetDeger(b.sayac));
        }

        if (_txtOzet != null)
            _txtOzet.text = $"{acik}/{_liste.basarilar.Count}   {Sayi(puan)}/{Sayi(_liste.ToplamPuan())} PTS";
    }

    void HandleClose()
    {
        if (SesKutusu.instance != null) SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }

    public static string Sayi(int deger) => deger.ToString("N0", CultureInfo.InvariantCulture);
}
