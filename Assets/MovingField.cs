using UnityEngine;

public class MovingField : MonoBehaviour
{
    [SerializeField] float moveAmount = 6f;
    [SerializeField] float moveSpeed = 0.4f;
    private float moveCheckerValue;
    private void FixedUpdate()
    {
        moveCheckerValue += moveSpeed;
        if (moveCheckerValue >= 6f || moveCheckerValue <= -6f)
        {
            moveSpeed = -moveSpeed;
        }
        transform.Translate(new Vector3(moveSpeed, 0, 0));
    }
}
