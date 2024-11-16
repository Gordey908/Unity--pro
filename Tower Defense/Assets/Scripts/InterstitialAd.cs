using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAd Instance { get; private set; }

    [SerializeField]
    private string _androidAdUnitId = "Interstitial_Android";
    [SerializeField]
    private string _iOsAdUnitId = "Interstitial_iOs";

    private string _adUnitId;
    private bool isLoaded = false;
    private int buildTowersCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ? _iOsAdUnitId : _androidAdUnitId;
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (isLoaded)
        {
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.LogWarning("Ad not loaded yet.");
        }
    }

    public void TowerWasBuild()
    {
        buildTowersCount++;
        if (buildTowersCount >= 5)
        {
            buildTowersCount = 0;
            ShowAd();
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == _adUnitId)
        {
            isLoaded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad failed to load: {adUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId == _adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Ad completed.");
        }
    }

    public void OnUnityAdsShowStart(string adUnitId) 
    { 

    }
    public void OnUnityAdsShowClick(string adUnitId) 
    {
    
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed: {adUnitId} - {error} - {message}");
    }
}
