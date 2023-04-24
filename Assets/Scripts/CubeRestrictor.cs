using UnityEngine;

public class CubeRestrictor : MonoBehaviour
{
    private bool firstEnter;
    public enum Restrict
    {
        left,
        right
    }
    public Restrict restrict;
    private void OnTriggerEnter(Collider other)
    {
        firstEnter = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube cube))
        {
            Transform transform = CubeController.Instance.gameObject.transform;
            if (restrict == Restrict.left)
            {
                if (firstEnter)
                    transform.Translate(new Vector3(0, 0, 0.03f));
                else
                    transform.Translate(new Vector3(0, 0, 0.1f));
            }
            else
            {
                if (firstEnter)
                    transform.Translate(new Vector3(0, 0, -0.03f));
                else
                    CubeController.Instance.gameObject.transform.Translate(new Vector3(0, 0, -0.1f));
            }
            firstEnter = false;
        }
    }
}
