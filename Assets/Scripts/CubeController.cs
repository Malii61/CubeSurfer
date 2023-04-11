using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public static CubeController Instance { get; private set; }
    public event EventHandler OnGameOver;
    [SerializeField] private float leftAndRightSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] CollectorCube collector;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        collector.OnCubeCollected += Collector_OnCubeCollected;
        collector.OnCubeDropped += Collector_OnCubeDropped;
    }

    private void Collector_OnCubeDropped(object sender, System.EventArgs e)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        collector.transform.position = new Vector3(collector.transform.position.x, collector.transform.position.y + 1, collector.transform.position.z);
    }

    private void Collector_OnCubeCollected(object sender, CollectorCube.OnCubeCollectedEventArgs e)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        collector.transform.position = new Vector3(collector.transform.position.x, collector.transform.position.y - 1, collector.transform.position.z);
    }
    private void OnDestroy()
    {
        collector.OnCubeCollected -= Collector_OnCubeCollected;
        collector.OnCubeDropped -= Collector_OnCubeDropped;
    }

    private void Update()
    {
        Move();
        ClampPosition();
    }
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * leftAndRightSpeed * Time.deltaTime;
        transform.Translate(-forwardSpeed * Time.deltaTime, 0, horizontalMove);
    }
    private void ClampPosition()
    {
        var zPos = transform.position.z;
        zPos = Mathf.Clamp(zPos, -4.5f, 4.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IObstacle obstacle))
        {
            Time.timeScale = 0;
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        else if(other.transform.TryGetComponent(out MagnetManager magnet))
        {
            magnet.Use();
        }
    }
}
