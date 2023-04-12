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
    public event EventHandler OnGameOver;
    private int cubeCount = 0;
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
            if (cube.transform.childCount > 0)
            {
                for(int i = 0; i < cube.transform.childCount; i++)
                {
                    CollectableCube childCube = cube.transform.GetChild(i).GetComponent<CollectableCube>();
                    SetCubeParentToMainCube(childCube);
                }
            }
            SetCubeParentToMainCube(cube);
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

    private void SetCubeParentToMainCube(CollectableCube collectedCube)
    {
        cubeCount++;
        collectedCube.collected = true;
        collectedCube.transform.parent = mainCube;
        collectedCube.transform.localPosition = new Vector3(0, -cubeCount, 0);
        Debug.Log(collectedCube.transform.localPosition);
        OnCubeCollected?.Invoke(this, new OnCubeCollectedEventArgs
        {
            cubeTransform = collectedCube.transform,
            collectedCube = collectedCube
        });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WallObstacle obstacle))
        {
            for (int i = 0; i < collidedObstacleAmount; i++)
            {
                OnCubeDropped?.Invoke(this, EventArgs.Empty);
            }
            collidedObstacleAmount = 0;
        }
    }

    private bool IsRunOutOfCubes()
    {
        return cubeCount < 1;
    }

    public void OnCollidedWithObstacle()
    {
        if (IsRunOutOfCubes())
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            cubeCount--;
            collidedObstacleAmount++;
        }

    }
    public void OnCollidedWithGoldMultiplier()
    {
        if (IsRunOutOfCubes())
        {
            OnFinished?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            cubeCount--;
            collidedGoldMultiplierAmount++;
        }
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
