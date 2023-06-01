using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnLevelCompleted;
    public event EventHandler OnGameOver;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CollectorCube.Instance.OnFinished += CollectorCube_OnFinished;
        CollectorCube.Instance.OnGameOver += CollectorCube_OnGameOver;
    }
    private void OnDisable()
    {
        CollectorCube.Instance.OnFinished -= CollectorCube_OnFinished;
        CollectorCube.Instance.OnGameOver -= CollectorCube_OnGameOver;
    }

    private void CollectorCube_OnGameOver(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    private void CollectorCube_OnFinished(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        OnLevelCompleted?.Invoke(this, EventArgs.Empty);
    }

}
