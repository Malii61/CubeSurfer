using UnityEngine;

public class CubeCreatorArea : MonoBehaviour
{
    [SerializeField] Transform pfCollectableCube;
    [SerializeField] float createCubeTimer = 0.2f;
    private float instantiateTimer;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube collector))
        {
            instantiateTimer += Time.fixedDeltaTime;
            if (instantiateTimer >= createCubeTimer)
            {
                Instantiate(pfCollectableCube, collector.transform.position, Quaternion.identity);
                instantiateTimer = 0f;
            }
        }
    }

}
