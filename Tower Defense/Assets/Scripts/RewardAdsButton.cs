using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField]
    private string _iOsAdUnitId = "Rewarded_iOs";
    private string _adUnitId;

    [SerializeField]
    private Button _showAdButton;

    private bool isLoaded = false;

    private void Awake()
    {
        _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ? _iOsAdUnitId : _androidAdUnitId;

        _showAdButton.interactable = false;
        _showAdButton.onClick.AddListener(ShowAd);

        LoadAd();
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == _adUnitId)
        {
            isLoaded = true;
            _showAdButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load ad {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId == _adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            GameController.Instance.AddCoins(100); // Награда за просмотр рекламы
        }
        LoadAd();
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

    private void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (isLoaded)
        {
            _showAdButton.interactable = false;
            Advertisement.Show(_adUnitId, this);
        }
    }
}
