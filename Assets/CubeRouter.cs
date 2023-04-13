using System.Collections;
using UnityEngine;
using Cinemachine;
public class CubeRouter : MonoBehaviour
{
    public enum Direction
    {
        left,
        right,
        back,
        forward
    }

    [SerializeField] Transform cinemachine;
    [SerializeField] Direction direction;
    private void Start()
    {
        if (!cinemachine)
        {
            cinemachine = FindObjectOfType<CinemachineVirtualCamera>().transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectorCube cube))
        {
            float rotationValue = 0f;
            switch (direction)
            {
                case Direction.right:
                    rotationValue = 90f;
                    break;

            }
            Transform mainCube = cube.transform.parent;
            StartCoroutine(RotateSmoothly(mainCube.rotation.eulerAngles.y, mainCube, rotationValue));
        }
    }
    private IEnumerator RotateSmoothly(float firstRotValue, Transform cube, float rotationValue)
    {
        while (firstRotValue + rotationValue != cube.rotation.eulerAngles.y)
        {
            Vector3 cubeVec = cube.rotation.eulerAngles;
            cube.rotation = Quaternion.Slerp(cube.rotation, Quaternion.Euler(cubeVec.x, cubeVec.y + rotationValue, cubeVec.z), 0.1f);

            Vector3 cmVec = cinemachine.rotation.eulerAngles;
            cinemachine.rotation = Quaternion.Slerp(cinemachine.rotation, Quaternion.Euler(cmVec.x, cmVec.y + rotationValue, cmVec.z), 0.1f);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Destroy(gameObject);
    }
}
