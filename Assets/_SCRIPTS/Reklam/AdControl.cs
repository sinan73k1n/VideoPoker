using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdControl : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdControl instance;
    string gameId = "4577695";
    string placementIdBanner = "Banner_Android";
    string placementIdOdul = "Rewarded_Android";
    string placementIdOdulYeniGorev = "Rewarded_Yeni_Gorev";
    bool testMode = false;
    int _hangiYeniGorev;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (!KAYIT.GetReklamVar()) return;
        Advertisement.Initialize(gameId, testMode);


    }
    public void ShowRewardedVideo()
    {
        if (!KAYIT.GetReklamVar())
        {
            CanvasCredits.instance.ReklamComplete();
            return;
        }
        Advertisement.Show(placementIdOdul, instance);
    }
    public void ShowRewardedVideoYeniGorev(int hangi)
    {
        if (!KAYIT.GetReklamVar()) return;

        _hangiYeniGorev = hangi;
        Advertisement.Show(placementIdOdulYeniGorev, instance);
    }
    public void ShowBanner()
    {
        if (!KAYIT.GetReklamVar()) return;

        StartCoroutine(ShowBannerWhenReady());
    }

    public void CloseBanner()
    {
        if (!KAYIT.GetReklamVar()) return;

        Advertisement.Banner.Hide();
    }

    IEnumerator ShowBannerWhenReady()
    {

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        yield return new WaitForSeconds(0f);

        Advertisement.Banner.Show(placementIdBanner);


    }

    public void OnInitializationComplete()
    {

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    public void OnUnityAdsAdLoaded(string placementId)
    {

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {


        if (placementId == placementIdOdulYeniGorev)
        {
            UI_YENI_GOREV.instance.Basarili(false, _hangiYeniGorev);
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                if (placementId == placementIdOdul)
                {
                    CanvasCredits.instance.ReklamComplete();
                }
                else if (placementId == placementIdOdulYeniGorev)
                {
                    UI_YENI_GOREV.instance.Basarili(true, _hangiYeniGorev);
                }


                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
            default:
                break;
        }
    }
}
