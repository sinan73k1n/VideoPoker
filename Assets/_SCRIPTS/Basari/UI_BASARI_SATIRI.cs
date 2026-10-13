using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Başarılar penceresindeki tek satır. Her alan isteğe bağlı (boşsa atlanır).
public class UI_BASARI_SATIRI : MonoBehaviour
{
    [SerializeField] Image      _imgIkon, _imgCerceve, _imgDolum;   // _imgDolum: Image Type = Filled
    [SerializeField] TMP_Text   _txtAd, _txtAciklama, _txtIlerleme;
    [SerializeField] GameObject _goKilit;
    [SerializeField] Sprite     _ikonVarsayilan;

    public void Ata(BASARI_TANIM b, bool acik, int deger)
    {
        bool gizle = b.gizli && !acik;
        Color renk = acik ? BASARI_RENK.Get(b.kademe) : BASARI_RENK.Kilitli;

        Yaz(_txtAd,       gizle ? "???" : b.ad);
        Yaz(_txtAciklama, gizle ? "SECRET ACHIEVEMENT" : b.aciklama);
        Yaz(_txtIlerleme, acik ? $"+{b.puan}" : gizle ? "" : $"{UI_BASARILAR.Sayi(Mathf.Min(deger, b.hedef))}/{UI_BASARILAR.Sayi(b.hedef)}");

        if (_imgIkon != null)
        {
            _imgIkon.sprite = b.ikon != null && !gizle ? b.ikon : _ikonVarsayilan;
            _imgIkon.color  = b.ikon != null && acik ? Color.white : renk;
        }
        if (_imgCerceve != null) _imgCerceve.color = renk;
        if (_imgDolum != null)   _imgDolum.fillAmount = acik ? 1f : b.hedef > 0 ? Mathf.Clamp01(deger / (float)b.hedef) : 0f;
        if (_goKilit != null)    _goKilit.SetActive(!acik);
    }

    static void Yaz(TMP_Text txt, string deger)
    {
        if (txt != null) txt.text = deger;
    }
}
