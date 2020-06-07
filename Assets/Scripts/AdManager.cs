using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour,IUnityAdsListener
{
    private string playStoreID = "3497105"; // just for googleplay
    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
    }
    private void InitializeAdvertisement()
    {
        if(isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd); return;
        }
    }
    public void PlayInterstitialAd()
    {
        if(!Advertisement.IsReady(interstitialAd))
        {
            return;
        }
        Advertisement.Show(interstitialAd);
    }

    public void RewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
            return;
        Advertisement.Show(rewardedVideoAd);

    }

    //disable all the throws
    public void OnUnityAdsReady(string placementId)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId) // need to mute music when ad play
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
     switch(showResult)
        {
            case ShowResult.Failed: break;
            case ShowResult.Skipped: break;
            case ShowResult.Finished:
                if(placementId==rewardedVideoAd)
                Debug.Log("reward player");
                if (placementId == interstitialAd)
                Debug.Log("ad finished");
                break;
        }
    }


}
