using System;
using UnityEngine;

public class MovingField : MonoBehaviour
{
    public enum MoveType
    {
        position,
        rotation,
        both
    }

    public MoveType moveType;

    public PositionParams positionParams;
    [Serializable]
    public class PositionParams
    {
        public Vector3 moveAmountMax;
        public Vector3 moveAmount;
    }

    public RotationParams rotationParams;
    [Serializable]
    public class RotationParams
    {
        //public Vector3 rotationAmount;
        public Vector3 rotationSpeed;
    }
    private Vector3 traveledPosition = Vector3.zero;

    public void CheckPositionParams()
    {
        if (Mathf.Abs(traveledPosition.x) >= Mathf.Abs(positionParams.moveAmountMax.x))
        {
            positionParams.moveAmount.x *= -1;
        }
        if (Mathf.Abs(traveledPosition.y) >= Mathf.Abs(positionParams.moveAmountMax.y))
        {
            positionParams.moveAmount.y *= -1;
        }
        if (Mathf.Abs(traveledPosition.z) >= Mathf.Abs(positionParams.moveAmountMax.z))
        {
            positionParams.moveAmount.z *= -1;
        }
        traveledPosition += positionParams.moveAmount;
    }
    private void FixedUpdate()
    {
        CheckParams();
    }

    private void CheckParams()
    {
        switch (moveType)
        {
            case MoveType.position:
                transform.Translate(positionParams.moveAmount);
                CheckPositionParams();
                break;
            case MoveType.rotation:
                transform.Rotate(rotationParams.rotationSpeed);
                break;
            case MoveType.both:
                transform.Translate(positionParams.moveAmount);
                CheckPositionParams();
                transform.Rotate(rotationParams.rotationSpeed);
                break;
        }

    }
}
