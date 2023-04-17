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
    private void OnTriggerEnter(Collider other)
    {
        pos = CubeController.Instance.gameObject.transform.position;
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
                    transform.position = new Vector3(pos.x, pos.y, pos.z + 0.05f);
                else
                    transform.Translate(new Vector3(0, 0, 0.1f));
            }
            else
            {
                if (firstEnter)
                    transform.position = new Vector3(pos.x, pos.y, pos.z - 0.05f);
                else
                    CubeController.Instance.gameObject.transform.Translate(new Vector3(0, 0, -0.1f));
            }
            firstEnter = false;
        }
    }
}
