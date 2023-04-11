using System;
using UnityEngine;

public class CollectorCube : MonoBehaviour
{
    public static CollectorCube Instance { get; private set; }
    [SerializeField] Transform mainCube;
    public event EventHandler<OnCubeCollectedEventArgs> OnCubeCollected;
    public class OnCubeCollectedEventArgs : EventArgs
    {
        public Transform cubeTransform;
        public CollectableCube collectedCube;
    }
    public event EventHandler OnCubeDropped;
    public event EventHandler OnCoinCollected;
    public event EventHandler OnFinished;
    private int cubeCount;
    private int collidedObstacleAmount;
    private int collidedGoldMultiplierAmount = 1;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectableCube cube))
        {
            if (cube.collected)
                return;
            cubeCount++;
            cube.collected = true;
            cube.transform.parent = mainCube;
            cube.transform.localPosition = new Vector3(0, -cubeCount, 0);
            OnCubeCollected?.Invoke(this, new OnCubeCollectedEventArgs
            {
                cubeTransform = cube.transform,
                collectedCube = cube
            });
        }
        else if (other.CompareTag("Coin"))
        {
            OnCoinCollected?.Invoke(this, EventArgs.Empty);
            Destroy(other.transform.parent.gameObject);
        }
        else if (other.CompareTag("FinishLine"))
        {
            OnFinished?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WallObstacle obstacle))
        {
            for(int i = 0; i < collidedObstacleAmount; i++)
            {
                cubeCount--;
                OnCubeDropped?.Invoke(this, EventArgs.Empty);
            }
            collidedObstacleAmount = 0;
        }
    }
    public void OnCollidedWithObstacle()
    {
        collidedObstacleAmount++;
    }
    public void OnCollidedWithGoldMultiplier()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        collidedGoldMultiplierAmount++;
    }
    public int GetGoldMultiplier()
    {
        return collidedGoldMultiplierAmount;
    }
    public Transform GetPosition()
    {
        return transform;
    }
}
