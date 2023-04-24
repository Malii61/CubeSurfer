using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CubeAmountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cubeCountText;
    private void LateUpdate()
    {
        cubeCountText.text = (CollectorCube.Instance.GetCubeCount() + 1).ToString();
    }
}
