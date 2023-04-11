using System.Collections;
using UnityEngine;

public class MagnetManager : MonoBehaviour, IPowerUp
{
    private float range = 3.5f;
    private float duration = 0f;
    private float durationMax = 10f;
    public void Use()
    {
        StartCoroutine(CheckAndMagnetizeCoins());
    }
    private IEnumerator CheckAndMagnetizeCoins()
    {
        transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        while (duration < durationMax)
        {
            Vector3 magnetizePos = CollectorCube.Instance.GetPosition().position;
            Collider[] colliders = Physics.OverlapSphere(magnetizePos, range);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Coin"))
                {
                    collider.transform.parent.position = Vector3.Lerp(collider.transform.parent.position, magnetizePos, Time.fixedDeltaTime * 15);
                }
            }
            duration += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        duration = 0;

    }
}
