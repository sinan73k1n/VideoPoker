using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

// BASARI_YONETICISI prefabında durur. Açılışta Play Games'e sessiz giriş yapar; başarırsa
// yöneticiye Google Play aynasını takar. Bileşen silinirse ya da giriş olmazsa (oyuncu reddetti,
// internet yok) başarılar yalnız oyun içinde kalır — oyun etkilenmez.
public class BASARI_PLAY_GIRIS : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(HandleGiris);
    }

    void HandleGiris(SignInStatus durum)
    {
        if (durum != SignInStatus.Success) return;
        if (BASARI_YONETICISI.instance != null)
            BASARI_YONETICISI.instance.SetPlatform(new BasariPlatformuGooglePlay());
    }
#endif
}
