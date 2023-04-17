using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum Axis
{
    None = 0, // Custom name for "Nothing" option
    A = 1 << 0,
    B = 1 << 1,
    AB = A | B, // Combination of two flags
    C = 1 << 2,
    All = ~0, // Custom name for "Everything" option
}
public class MovingAndSpinningField : MonoBehaviour
{
    [SerializeField] float spinAmount = 0.5f;
    public Axis axis;
    //public bool WanderAround;
    //[ConditionalField("WanderAround")] public float WanderDistance = 5;

    //public AIState NextState = AIState.None;
    //[ConditionalField("NextState", AIState.Idle)] public float IdleTime = 5;
    public enum MoveDirection
    {
        rightToLeft,
        leftToRight,
    }
    [SerializeField] MoveDirection moveDirection;
    [SerializeField] float moveAmount = 1f;
    [SerializeField] float moveSpeed = 0.15f;
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
        transform.Rotate(new Vector3(0, 0, spinAmount));
    }
}
