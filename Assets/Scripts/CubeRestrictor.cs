using UnityEngine;

public class CubeRestrictor : MonoBehaviour
{
    private Vector3 pos;
    private bool firstEnter;
    public enum Restrict
    {
        left,
        right
    }
    public Restrict restrict;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube cube))
        {

            if (restrict == Restrict.left)
            {
                CubeController.Instance.gameObject.transform.Translate(new Vector3(0, 0, 0.5f));
            }
            else
            {
                CubeController.Instance.gameObject.transform.Translate(new Vector3(0, 0, -0.5f));
            }
            Debug.Log("it works");
        }
    }
}
