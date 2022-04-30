using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[ExecuteAlways]
public class CameraZoom : MonoBehaviour
{
    public float horizontalResolution = 1920;

    void OnGUI()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = horizontalResolution / currentAspect / 200;
    }
}
