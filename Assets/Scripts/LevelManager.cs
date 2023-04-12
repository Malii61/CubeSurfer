using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void OnClick_LevelButton(int level)
    {
        SceneManager.LoadScene(level + 1);
    }
}
