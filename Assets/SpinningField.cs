using UnityEngine;

public class SpinningField : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0.3f));
    }
}
