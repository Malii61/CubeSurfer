using System.Collections;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] private float acceleratorValue = 3f;
    [SerializeField] private float accelerateTimer = 2f;
    private float firstSpeed;
    private bool accelerateOnce = true;
    public enum Direction
    {
        same,
        opposite
    }
    public Direction direction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube collector) && accelerateOnce)
        {
            CubeController cube = CubeController.Instance;
            firstSpeed = cube.GetCubeSpeed();
            switch (direction)
            {
                case Direction.same:
                    cube.SetCubeSpeed(cube.GetCubeSpeed() + acceleratorValue);
                    break;
                case Direction.opposite:
                    cube.SetCubeSpeed(cube.GetCubeSpeed() - acceleratorValue);
                    break;
            }
            StartCoroutine(SpeedTimer(firstSpeed));
        }
    }
    private IEnumerator SpeedTimer(float firstSpeed)
    {
        accelerateOnce = false;
        yield return new WaitForSeconds(accelerateTimer);

        CubeController.Instance.SetCubeSpeed(firstSpeed);
        accelerateOnce = true;
    }
}
