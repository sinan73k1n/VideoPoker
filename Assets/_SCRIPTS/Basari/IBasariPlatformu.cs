// Google Play Games / Game Center aynası. Gerçeğin kaynağı oyun içi kayıttır (KAYIT_BASARI);
// platform yalnız kopyasını alır. Çağrılar idempotent olmalı: aynı başarı iki kez açılırsa sorun yok.
public interface IBasariPlatformu
{
    void Ac(BASARI_TANIM basari);
    void IlerlemeBildir(BASARI_TANIM basari, int deger);
}

// Platform yokken (editör, Faz 1, oturum açılmadı) — hiçbir şey yapmaz.
public class BasariPlatformuYok : IBasariPlatformu
{
    public void Ac(BASARI_TANIM basari) { }
    public void IlerlemeBildir(BASARI_TANIM basari, int deger) { }
}
