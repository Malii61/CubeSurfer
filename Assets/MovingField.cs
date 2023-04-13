using UnityEngine;

public class MovingField : MonoBehaviour
{
    public enum MoveDirection
    {
        rightToLeft,
        leftToRight,
    }
    [SerializeField] MoveDirection moveDirection;
    [SerializeField] float moveAmount = 6f;
    [SerializeField] float moveSpeed = 0.2f;
    private float moveCheckerValue;
    private void Start()
    {
        if (moveDirection == MoveDirection.rightToLeft)
            moveSpeed *= -1;
    }
    private void FixedUpdate()
    {
        moveCheckerValue += moveSpeed;
        if (moveCheckerValue >= moveAmount || moveCheckerValue <= -moveAmount)
        {
            moveSpeed *= -1;
        }
        transform.Translate(new Vector3(moveSpeed, 0, 0));
    }
}
