#if UNITY_ANDROID
using GooglePlayGames;
using UnityEngine;

// Google Play Games "Hedefler" aynası. Kimlik (androidId) boşsa atlar: liste Play Console'a
// girilmeden de oyun hatasız çalışır. Çağrılar idempotent — açık başarıyı yeniden açmak, aynı
// adımı yeniden göndermek Play tarafında hiçbir şeyi değiştirmez.
public class BasariPlatformuGooglePlay : IBasariPlatformu
{
    public void Ac(BASARI_TANIM basari)
    {
        if (string.IsNullOrEmpty(basari.androidId)) return;
        PlayGamesPlatform.Instance.UnlockAchievement(basari.androidId);
    }

    public void IlerlemeBildir(BASARI_TANIM basari, int deger)
    {
        if (string.IsNullOrEmpty(basari.androidId) || !basari.PlayArtimli || deger <= 0) return;
        // SetStepsAtLeast: Play'deki adım bundan küçükse yükseltir, büyükse dokunmaz (geri gitmez, şişmez).
        PlayGamesPlatform.Instance.SetStepsAtLeast(basari.androidId, Mathf.Min(deger, basari.hedef), null);
    }
}
#endif
