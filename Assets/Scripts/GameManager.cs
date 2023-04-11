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
        CollectorCube.Instance.OnFinished += CubeCollector_OnFinished;
        CubeController.Instance.OnGameOver += Instance_OnGameOver;
    }

    private void Instance_OnGameOver(object sender, EventArgs e)
    {
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    private void CubeCollector_OnFinished(object sender, EventArgs e)
    {
        OnLevelCompleted?.Invoke(this, EventArgs.Empty);
    }
    private void OnDestroy()
    {
        CollectorCube.Instance.OnFinished -= CubeCollector_OnFinished;
    }

}
