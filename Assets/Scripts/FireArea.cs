using UnityEngine;
using System.Collections;

public class FireArea : MonoBehaviour, IObstacle
{
    [SerializeField] AudioClip burnCubeSound;
    public void OnCollision(CollectableCube cube)
    {
        StartCoroutine(BurnCube(cube));
        CollectorCube.Instance.PlayAudioClip(burnCubeSound);
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
