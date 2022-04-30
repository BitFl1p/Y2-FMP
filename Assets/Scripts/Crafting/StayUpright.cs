using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class StayUpright : MonoBehaviour
{
    public Vector3 uprightAngle;
    void Update()
    {
        transform.eulerAngles = uprightAngle;
    }
}
