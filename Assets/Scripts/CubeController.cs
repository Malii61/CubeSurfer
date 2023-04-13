using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public enum PositionClamp
    {
        left,
        right,
        none
    }
    private PositionClamp positonClamp = PositionClamp.none;
    public static CubeController Instance { get; private set; }
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

    private void Collector_OnCubeDropped(object sender, EventArgs e)
    {
        UpdatePositions();
    }
    private void Collector_OnCubeCollected(object sender, CollectorCube.OnCubeCollectedEventArgs e)
    {
        UpdatePositions();
    }
    private void UpdatePositions()
    {
        int childCount = transform.childCount;
        transform.position = new Vector3(transform.position.x, childCount - 1, transform.position.z);
        transform.GetChild(1).localPosition = new Vector3(0, -(childCount - 2), 0);
        for (int i = 2; i < childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, -(i - 1), 0);
        }
    }


    private void OnDestroy()
    {
        collector.OnCubeCollected -= Collector_OnCubeCollected;
        collector.OnCubeDropped -= Collector_OnCubeDropped;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * leftAndRightSpeed * Time.deltaTime;
        float adjustedHorizontalMove = CheckPositionClamper(horizontalMove);
        transform.Translate(-forwardSpeed * Time.deltaTime, 0, adjustedHorizontalMove);
    }

    private float CheckPositionClamper(float horizontalMove)
    {
        bool isGreaterThanZero = horizontalMove > 0;
        if ((positonClamp == PositionClamp.right && isGreaterThanZero) || (positonClamp == PositionClamp.left && !isGreaterThanZero))
            return 0f;
        return horizontalMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out MagnetManager magnet))
        {
            magnet.Use();
        }

        if (other.TryGetComponent(out WallObstacle obstacle))
        {
            collector.OnCollidedWithObstacle();
        }
        else if (other.TryGetComponent(out GoldMultiplier goldMultiplier))
        {
            collector.OnCollidedWithGoldMultiplier();
        }
    }
    public void SetClampPosition(PositionClamp clamp)
    {
        positonClamp = clamp;
    }
}