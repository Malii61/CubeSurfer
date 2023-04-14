using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralCoinManager : MonoBehaviour
{
    public static GeneralCoinManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI coinText;
    private int coin;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        coinText.enabled = false;
    }
    public async void GetCoinFromCloud()
    {
        coin = await CloudSave.Load<int>("coin");
        coinText.enabled = true;
        coinText.text = coin.ToString();
    }
    public int GetCoin()
    {
        return coin;
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex == 1)
        {
            GetCoinFromCloud();
        }
    }
}
