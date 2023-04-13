using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    internal bool collected;
    private bool triggeredWithObstacle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallObstacle obstacle) && !triggeredWithObstacle)
        {
            triggeredWithObstacle = true;
            CollectorCube.Instance.OnCollidedWithObstacle();
            SetPositionAndDestroy(obstacle.transform);
        }
        else if(other.TryGetComponent(out GoldMultiplier goldMultiplier))
        {
            CollectorCube.Instance.OnCollidedWithGoldMultiplier();
            SetPositionAndDestroy(goldMultiplier.transform);
        }
    }
    private void SetPositionAndDestroy(Transform collidedTransform)
    {
        transform.parent = null;
        transform.position = new Vector3(transform.position.x, collidedTransform.position.y, transform.position.z);
        Destroy(GetComponent<BoxCollider>());
        Destroy(gameObject, 5f);
    }
}
