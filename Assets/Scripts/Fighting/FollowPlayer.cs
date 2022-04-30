using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{
    public bool XLocked, YLocked;
    public Vector2 offset;
    public Transform followTarget;

    void Update() => transform.position = new Vector2(XLocked ? offset.x : followTarget.position.x + offset.x, YLocked ? offset.y : followTarget.position.y + offset.y);
}
