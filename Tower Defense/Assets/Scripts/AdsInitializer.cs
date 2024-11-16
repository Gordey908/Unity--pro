using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOSGameId;
    [SerializeField] private bool _testMode = true;

    private string _gameId;

    public static AdsInitializer Instance { get; private set; }

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
            return;
        }

        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#else
        _gameId = null;
#endif

        if (!Advertisement.isInitialized && Advertisement.IsSupported())
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else
        {
            Debug.LogWarning("Unity Ads уже инициализированы или платформа не поддерживается.");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads успешно инициализированы.");

        if (InterstitialAd.Instance != null)
        {
            InterstitialAd.Instance.LoadAd();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Ошибка инициализации Unity Ads: {error} - {message}");
    }
}
