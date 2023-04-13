using UnityEngine;
using System.Collections;

public class FireArea : MonoBehaviour, IObstacle
{
    public void OnCollision(CollectableCube cube)
    {
        StartCoroutine(BurnCube(cube));
    }
    public void AfterCollision(CollectableCube cube)
    {
    }
    private IEnumerator BurnCube(CollectableCube collectableCube)
    {
        yield return new WaitForSeconds(0.12f);
        collectableCube.SetPositionAndDestroy(transform, 0);
        CollectorCube.Instance.DropCubeManually();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube collector))
        {
            collector.CheckGameOver();
        }
    }
}
