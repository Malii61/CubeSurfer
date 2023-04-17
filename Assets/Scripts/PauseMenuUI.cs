using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    private const string SENSITIVITY = "sensitivity";

    [SerializeField] Transform settingsPanel;
    [SerializeField] Scrollbar sensitivityScrollbar;
    public static bool isPauseMenuActive;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(SENSITIVITY))
        {
            sensitivityScrollbar.value = PlayerPrefs.GetFloat(SENSITIVITY);
        }
        gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        isPauseMenuActive = false;
    }
    public void OnClick_PauseButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0;
            isPauseMenuActive = true;
        }
        else
        {
            Time.timeScale = 1;
            isPauseMenuActive = false;
        }
    }
    public void OnClick_ResumeButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        isPauseMenuActive = false;
    }
    public void OnClick_SettingsButton()
    {
        gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }
    public void OnClick_BackButton()
    {
        gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
    }
    public void OnClick_RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClick_ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void OnSensitivityValueChanged()
    {
        int val = (int)(sensitivityScrollbar.value * 10f);
        PlayerPrefs.SetFloat(SENSITIVITY, sensitivityScrollbar.value);
        CubeController.Instance?.SetSensitivityMultiplier(val);
    }
}
