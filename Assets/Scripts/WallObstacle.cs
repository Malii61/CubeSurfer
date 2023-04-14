using UnityEngine;

public class WallObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] AudioClip collideWithWallSound;
    public void OnCollision(CollectableCube cube)
    {
        cube.SetPositionAndDestroy(transform, checkPositions: true);
        CollectorCube.Instance.OnCollidedWithObstacle();
        CollectorCube.Instance.PlayAudioClip(collideWithWallSound);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CubeController controller))
        {
            CollectorCube.Instance.CheckGameOver();
        }
    }
}
