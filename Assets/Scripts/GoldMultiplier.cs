using UnityEngine;

public class GoldMultiplier : MonoBehaviour, IObstacle
{
    public void OnCollision(CollectableCube cube)
    {
        CollectorCube.Instance.OnCollidedWithGoldMultiplier();
        cube.SetPositionAndDestroy(transform, 5f);
    }
}
