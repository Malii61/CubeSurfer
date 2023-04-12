//using System.Collections;
//using UnityEngine;

//public class CubeRouter : MonoBehaviour
//{
//    [SerializeField] Transform cinemachine;
//    float rotateTimer = 0f;
//    float rotateTimerMax = 0.5f;
//    public enum Direction
//    {
//        left,
//        right,
//        back,
//        forward
//    }
//    [SerializeField] Direction direction;
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.transform.TryGetComponent(out CollectorCube cube))
//        {
//            float rotationValue = 0f;
//            switch (direction)
//            {
//                case Direction.right:
//                    rotationValue = 90f;
//                    break;

//            }
//            cube.transform.parent.Rotate(new Vector2(0, rotationValue));
//            Vector3 convertedQuat = cinemachine.rotation.eulerAngles;
//            Debug.Log(convertedQuat);
//            cinemachine.Rotate(new Vector3(0, rotationValue, 0));
//            cinemachine.rotation = Quaternion.Euler(26, cinemachine.rotation.y, 0);
//            Debug.Log(cinemachine.rotation.y);
//            Destroy(gameObject);
//        }
//    }
//}
using System.Collections;
using UnityEngine;

public class CubeRouter : MonoBehaviour
{
    [SerializeField] Transform cinemachine;
    float rotateTimer = 0f;
    float rotateTimerMax = 2f;
    public enum Direction
    {
        left,
        right,
        back,
        forward
    }
    [SerializeField] Direction direction;
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
            rotateTimer += Time.fixedDeltaTime;

            Vector3 cubeVec = cube.rotation.eulerAngles;
            cube.rotation = Quaternion.Slerp(cube.rotation, Quaternion.Euler(cubeVec.x, cubeVec.y + rotationValue, cubeVec.z), 0.1f);

            Vector3 cmVec = cinemachine.rotation.eulerAngles;
            cinemachine.rotation = Quaternion.Slerp(cinemachine.rotation, Quaternion.Euler(cmVec.x, cmVec.y + rotationValue, cmVec.z), 0.1f);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Destroy(gameObject);
    }
}
