using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Başarı açılınca kısa süre görünen bildirim. Kendini yok eder; dokununca erken kapanır.
public class UI_YENI_BASARI : MonoBehaviour
{
    [SerializeField] Image    _imgIkon, _imgCerceve;
    [SerializeField] TMP_Text _txtBaslik, _txtAd;
    [SerializeField] Button   _btnKapat;
    [SerializeField] Sprite   _ikonVarsayilan;
    [SerializeField] float    _sure = 2.5f;

    void Awake()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas != null) canvas.sortingOrder = 30;
    }

    void Start()
    {
        if (_btnKapat != null) _btnKapat.onClick.AddListener(() => Destroy(gameObject));
        if (SesKutusu.instance != null) SesKutusu.instance.Play(NameOfAudioClip.Coin1);
        Destroy(gameObject, _sure);
    }

    public void Goster(BASARI_TANIM b)
    {
        Yaz(_txtBaslik, "ACHIEVEMENT UNLOCKED!");
        Yaz(_txtAd, b.ad);
        Boya(b.ikon, BASARI_RENK.Get(b.kademe));
    }

    public void GosterOzet(int sayi)
    {
        Yaz(_txtBaslik, $"{sayi} ACHIEVEMENTS UNLOCKED!");
        Yaz(_txtAd, "YOUR PAST GAMES COUNT TOO");
        Boya(null, BASARI_RENK.Get(BASARI_KADEME.Altin));
    }

    void Boya(Sprite ikon, Color renk)
    {
        if (_imgIkon != null)
        {
            _imgIkon.sprite = ikon != null ? ikon : _ikonVarsayilan;
            if (ikon == null) _imgIkon.color = renk;
        }
        if (_imgCerceve != null) _imgCerceve.color = renk;
    }

    static void Yaz(TMP_Text txt, string deger)
    {
        if (txt != null) txt.text = deger;
    }
}
