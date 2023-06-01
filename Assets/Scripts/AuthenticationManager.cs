using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationManager : MonoBehaviour
{
    private AsyncOperation _scene;

    private void Start()
    {
        AnonymousLogin();
    }
    public async void AnonymousLogin()
    {
        _scene = SceneManager.LoadSceneAsync("MenuScene");
        _scene.allowSceneActivation = false;
        await AuthService.LoginAnonymously();
        _scene.allowSceneActivation = true;
    }

    public void GooglePlayLoginClicked()
    {
        Debug.Log("I don't have google play account settings for now :)");
    }
}