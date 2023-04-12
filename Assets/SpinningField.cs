using UnityEngine;

public class SpinningField : MonoBehaviour
{
    [SerializeField] float spinAmount = 0.5f;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, spinAmount));
    }
}
