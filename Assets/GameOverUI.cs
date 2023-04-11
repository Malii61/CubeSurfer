using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        gameObject.SetActive(false);
    }
    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    public void OnClick_PlayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
