using UnityEngine;
using System.Collections;

public class FireArea : MonoBehaviour
{
    private bool burn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube collector))
        {
            burn = true;
            StartCoroutine(BurnCubes(collector));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube collector))
        {
            burn = false;
        }
    }
    private IEnumerator BurnCubes(CollectorCube collector)
    {
        while (burn)
        {
            collector.DropCubeManually();
            yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        }
    }
}
