using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class ParallaxNoTile : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public Vector2 parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;


    }
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxEffect.x, deltaMovement.y * parallaxEffect.y);
        lastCameraPosition = cameraTransform.position;


    }
}