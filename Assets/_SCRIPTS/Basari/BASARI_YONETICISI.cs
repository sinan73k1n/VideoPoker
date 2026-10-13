using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prefab olarak sahneye konur. Sahnede yoksa / silinirse oyun aynen çalışır:
// oyun bu sınıfı tanımaz, yalnız OYUN_OLAYLARI'na "şu oldu" der.
public class BASARI_YONETICISI : MonoBehaviour
{
    public static BASARI_YONETICISI instance;
    public static event Action<BASARI_TANIM> BasariAcildi;

    [SerializeField] BASARI_LISTESI _liste;
    [SerializeField] UI_YENI_BASARI _uiYeniBasari;   // boşsa bildirim gösterilmez

    IBasariPlatformu _platform = new BasariPlatformuYok();
    readonly Queue<BASARI_TANIM> _bekleyen = new Queue<BASARI_TANIM>();
    bool _bildirimGosteriliyor;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        OYUN_OLAYLARI.ElBasladi        += HandleElBasladi;
        OYUN_OLAYLARI.ElBitti          += HandleElBitti;
        OYUN_OLAYLARI.GorevOdulAlindi  += HandleGorevOdul;
        OYUN_OLAYLARI.KartArkasiAcildi += HandleKartArkasi;
    }

    void OnDisable()
    {
        OYUN_OLAYLARI.ElBasladi        -= HandleElBasladi;
        OYUN_OLAYLARI.ElBitti          -= HandleElBitti;
        OYUN_OLAYLARI.GorevOdulAlindi  -= HandleGorevOdul;
        OYUN_OLAYLARI.KartArkasiAcildi -= HandleKartArkasi;
    }

    void Start()
    {
        if (_liste == null) return;

        // İlk açılış: eski sayaçlarla hak edilenler tek tek değil, tek özet bildirimle açılır.
        bool ilkTarama = !KAYIT_BASARI.GetIlkTaramaYapildi();
        int acilan = Kontrol(!ilkTarama);
        if (ilkTarama)
        {
            KAYIT_BASARI.SetIlkTaramaYapildi();
            if (acilan > 0) OzetGoster(acilan);
        }
        PlatformaGonder();
    }

    public void SetPlatform(IBasariPlatformu platform)
    {
        _platform = platform ?? new BasariPlatformuYok();
        PlatformaGonder();
    }

    void HandleElBasladi(int bahis)
    {
        KAYIT_BASARI.AddToplamBahis(bahis);
        Kontrol(true);
    }

    void HandleElBitti(EL_SONUCU sonuc)
    {
        if (sonuc.kazanc > 0) KAYIT_BASARI.AddToplamKazanc(sonuc.kazanc);
        KAYIT_BASARI.SeriGuncelle(sonuc.kazanc > 0);

        if (sonuc.carpan == 250 && sonuc.bahis >= 5)           KAYIT_BASARI.SetOzel(BASARI_SAYAC.OzelMaxBahisRoyal);
        if (sonuc.carpan == 250 && sonuc.ilkDagitimCarpan == 250) KAYIT_BASARI.SetOzel(BASARI_SAYAC.OzelIlkDagitimRoyal);
        if (sonuc.carpan == 25 && DortAs(sonuc.kartlar))       KAYIT_BASARI.SetOzel(BASARI_SAYAC.OzelDortAs);

        Kontrol(true);
    }

    void HandleGorevOdul()
    {
        KAYIT_BASARI.AddGorevOdul();
        Kontrol(true);
    }

    void HandleKartArkasi() => Kontrol(true);

    // Hedefe ulaşmış ama açılmamış başarıları açar; kaç tane açıldığını döner.
    int Kontrol(bool bildir)
    {
        if (_liste == null) return 0;
        int acilan = 0;
        foreach (var b in _liste.basarilar)
        {
            if (b == null || string.IsNullOrEmpty(b.id) || KAYIT_BASARI.GetAcik(b.id)) continue;
            if (KAYIT_BASARI.GetDeger(b.sayac) < b.hedef) continue;

            KAYIT_BASARI.SetAcik(b.id);
            _platform.Ac(b);
            acilan++;
            try { BasariAcildi?.Invoke(b); } catch (Exception e) { Debug.LogException(e); }
            if (bildir) Bildir(b);
        }
        return acilan;
    }

    // Platform idempotent: açıkları yeniden açar, açık olmayanların ilerlemesini bildirir.
    void PlatformaGonder()
    {
        if (_liste == null) return;
        foreach (var b in _liste.basarilar)
        {
            if (b == null || string.IsNullOrEmpty(b.id)) continue;
            if (KAYIT_BASARI.GetAcik(b.id)) _platform.Ac(b);
            else if (b.hedef > 1)          _platform.IlerlemeBildir(b, KAYIT_BASARI.GetDeger(b.sayac));
        }
    }

    static bool DortAs(string kartlar)
    {
        if (string.IsNullOrEmpty(kartlar) || kartlar.Length < 20) return false;
        int as_ = 0;
        for (int i = 0; i < 5; i++)
            if (kartlar.Substring(i * 4 + 2, 2) == "01") as_++;
        return as_ == 4;
    }

    void Bildir(BASARI_TANIM b)
    {
        if (_uiYeniBasari == null) return;
        _bekleyen.Enqueue(b);
        if (!_bildirimGosteriliyor) StartCoroutine(crBildirimler());
    }

    void OzetGoster(int sayi)
    {
        if (_uiYeniBasari == null) return;
        Instantiate(_uiYeniBasari).GosterOzet(sayi);
    }

    IEnumerator crBildirimler()
    {
        _bildirimGosteriliyor = true;
        while (_bekleyen.Count > 0)
        {
            var ui = Instantiate(_uiYeniBasari);
            ui.Goster(_bekleyen.Dequeue());
            yield return new WaitWhile(() => ui != null);
        }
        _bildirimGosteriliyor = false;
    }
}
