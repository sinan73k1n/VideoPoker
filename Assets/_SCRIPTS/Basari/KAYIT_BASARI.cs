using UnityEngine;

// Başarı modülünün kaydı. ⛔ Yalnız YENİ anahtarlar — KAYIT.cs'teki eski anahtarlara yazmaz.
// Eski sayaçlar (OYNANAN_*) yalnız OKUNUR → güncellemeyi alan oyuncunun hak ettiği rozetler açılır.
public static class KAYIT_BASARI
{
    const string ACIK            = "BASARI_ACIK_";
    const string OZEL            = "BASARI_OZEL_";
    const string ILK_TARAMA      = "BASARI_ILK_TARAMA";
    const string TOPLAM_KAZANC   = "TOPLAM_KAZANC";
    const string TOPLAM_BAHIS    = "TOPLAM_BAHIS";
    const string SERI            = "KAZANC_SERISI";
    const string SERI_EN_IYI     = "KAZANC_SERISI_EN_IYI";
    const string GOREV_ODUL      = "GOREV_ODUL_SAYISI";

    const int KART_ARKASI_SAYISI = 22;   // KART_SATIS._keys uzunluğu
    const int BEDAVA_KART_ARKASI = 2;    // ilk ikisi baştan açık (-1)

    public static bool GetAcik(string id)        => PlayerPrefs.GetInt(ACIK + id, 0) == 1;
    public static void SetAcik(string id)        => PlayerPrefs.SetInt(ACIK + id, 1);
    public static void Kilitle(string id)        => PlayerPrefs.DeleteKey(ACIK + id);   // yalnız debug

    public static bool GetIlkTaramaYapildi()     => PlayerPrefs.GetInt(ILK_TARAMA, 0) == 1;
    public static void SetIlkTaramaYapildi()     => PlayerPrefs.SetInt(ILK_TARAMA, 1);

    public static int  GetToplamKazanc()         => PlayerPrefs.GetInt(TOPLAM_KAZANC, 0);
    public static void AddToplamKazanc(int v)    => PlayerPrefs.SetInt(TOPLAM_KAZANC, Tavan(GetToplamKazanc(), v));
    public static int  GetToplamBahis()          => PlayerPrefs.GetInt(TOPLAM_BAHIS, 0);
    public static void AddToplamBahis(int v)     => PlayerPrefs.SetInt(TOPLAM_BAHIS, Tavan(GetToplamBahis(), v));

    public static int  GetSeri()                 => PlayerPrefs.GetInt(SERI, 0);
    public static int  GetSeriEnIyi()            => PlayerPrefs.GetInt(SERI_EN_IYI, 0);
    public static void SeriGuncelle(bool kazandi)
    {
        int seri = kazandi ? GetSeri() + 1 : 0;
        PlayerPrefs.SetInt(SERI, seri);
        if (seri > GetSeriEnIyi()) PlayerPrefs.SetInt(SERI_EN_IYI, seri);
    }

    public static int  GetGorevOdulSayisi()      => PlayerPrefs.GetInt(GOREV_ODUL, 0);
    public static void AddGorevOdul()            => PlayerPrefs.SetInt(GOREV_ODUL, GetGorevOdulSayisi() + 1);

    public static bool GetOzel(BASARI_SAYAC s)   => PlayerPrefs.GetInt(OZEL + s, 0) == 1;
    public static void SetOzel(BASARI_SAYAC s)   => PlayerPrefs.SetInt(OZEL + s, 1);

    public static int GetAcilanKartArkasi()
    {
        int acik = 0;
        for (int i = 0; i < KART_ARKASI_SAYISI; i++)
            if (KART_SATIS.GetPrice(i) <= 0) acik++;
        return Mathf.Max(0, acik - BEDAVA_KART_ARKASI);
    }

    // Sayacın şu anki değeri — UI ve yönetici aynı yerden okur.
    public static int GetDeger(BASARI_SAYAC sayac)
    {
        switch (sayac)
        {
            case BASARI_SAYAC.OynananEl:      return KAYIT.GetCountOf_GAME();
            case BASARI_SAYAC.RoyalFlush:     return KAYIT.GetCountOfHand(TypeOfPokerHand.RoyalFlush);
            case BASARI_SAYAC.StraightFlush:  return KAYIT.GetCountOfHand(TypeOfPokerHand.StraightFlush);
            case BASARI_SAYAC.FourOfAKind:    return KAYIT.GetCountOfHand(TypeOfPokerHand.FourOfAKind);
            case BASARI_SAYAC.FullHouse:      return KAYIT.GetCountOfHand(TypeOfPokerHand.FullHouse);
            case BASARI_SAYAC.Flush:          return KAYIT.GetCountOfHand(TypeOfPokerHand.Flush);
            case BASARI_SAYAC.Straight:       return KAYIT.GetCountOfHand(TypeOfPokerHand.Straight);
            case BASARI_SAYAC.ThreeOfAKind:   return KAYIT.GetCountOfHand(TypeOfPokerHand.ThreeOfAKind);
            case BASARI_SAYAC.TwoPair:        return KAYIT.GetCountOfHand(TypeOfPokerHand.TwoPair);
            case BASARI_SAYAC.JacksOrBetter:  return KAYIT.GetCountOfHand(TypeOfPokerHand.JackOrBetter);
            case BASARI_SAYAC.ToplamKazanc:   return GetToplamKazanc();
            case BASARI_SAYAC.KazancSerisi:   return GetSeriEnIyi();
            case BASARI_SAYAC.GorevOdulu:     return GetGorevOdulSayisi();
            case BASARI_SAYAC.KartArkasi:     return GetAcilanKartArkasi();
            default:                          return GetOzel(sayac) ? 1 : 0;
        }
    }

    static int Tavan(int mevcut, int ekle)
    {
        long toplam = (long)mevcut + ekle;
        return toplam > int.MaxValue ? int.MaxValue : (int)toplam;
    }
}
