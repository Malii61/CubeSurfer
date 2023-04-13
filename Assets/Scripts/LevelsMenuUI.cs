using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelsMenuUI : MonoBehaviour
{
    [SerializeField] Transform levelMenu;
    public void OnClick_LevelButton(int level)
    {
        SceneManager.LoadScene(level + 1);
    }
    public void OnClick_OpenLevelMenuButton()
    {
        levelMenu.gameObject.SetActive(true);
    }
    public void OnClick_BackButton()
    {
        levelMenu.gameObject.SetActive(false);
    }
}
