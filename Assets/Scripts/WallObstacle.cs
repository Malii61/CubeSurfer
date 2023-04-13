using UnityEngine;

public class WallObstacle : MonoBehaviour, IObstacle
{
    public void OnCollision(CollectableCube cube)
    {
        CollectorCube.Instance.OnCollidedWithObstacle();
        cube.SetPositionAndDestroy(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CubeController controller))
        {
            CollectorCube.Instance.CheckGameOver();
        }
    }
    public void AfterCollision(CollectableCube cube)
    {
        //CollectorCube.Instance.OnCollidedWithObstacle();
    }

}
