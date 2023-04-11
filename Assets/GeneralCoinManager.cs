using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class GeneralCoinManager : MonoBehaviour
{
    public static GeneralCoinManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI coinText;
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
    public async void GetCoin()
    {
        var res = await CloudSave.Load<int>("coin");
        Debug.Log(res);
        coinText.text = res.ToString();
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
        if (arg1.buildIndex == 0)
        {
            GetCoin();
        }
    }
}
