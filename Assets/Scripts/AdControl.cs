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
    public bool testMode = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);


    }
    public void ShowRewardedVideo()
    {

        Advertisement.Show(placementIdOdul, instance);
    }
    public void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    public void CloseBanner()
    {
        Advertisement.Banner.Hide();
    }

    IEnumerator ShowBannerWhenReady()
    {
       
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        yield return new WaitForSeconds(0.0f);
        
        Advertisement.Banner.Show(placementIdBanner);
        Advertisement.Banner.Show(placementIdBanner);
     
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text = $"Advertisement.Banner.isLoaded: {Advertisement.Banner.isLoaded}";
    }

    public void OnInitializationComplete()
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text=
        ("OnInitializationComplete: ");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
("OnInitializationFailed: " + " " + error + " message: " + message);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
"OnUnityAdsAdLoaded: " + placementId;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
("OnUnityAdsFailedToLoad: " + placementId + " " + error + " message: " + message);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
("OnUnityAdsShowFailure: " + placementId + " " + error + " message: " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
("OnUnityAdsShowStart: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GameObject.Find("DEBUG").GetComponentInChildren<Text>().text =
("OnUnityAdsShowComplete: " + placementId+showCompletionState);
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                GameManagerVideoPoker.instance.AddCredits(50);
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
            default:
                break;
        }
    }
}
