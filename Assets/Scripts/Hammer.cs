using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform origin;
    Rigidbody2D rb;
    public float moveSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //transform.LookAt(origin);
        Vector3 targ = origin.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity += (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
    }
}
