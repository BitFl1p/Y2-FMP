using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowMouse : MonoBehaviour
{
    public Transform origin;
    internal Rigidbody2D rb;
    public float moveSpeed;
    public bool clickedOn;
    internal Vector2 start;
    public float zOffset;
    internal virtual void Start()
    {
        start = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    internal virtual void Update()
    {
        Vector3 targ = origin.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        if (Input.GetMouseButton(0))
        {
            if (!clickedOn)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if(hit.transform) if (hit.transform.gameObject == gameObject) clickedOn = true;
            }
            else
            {
                rb.gravityScale = 0f;
                rb.velocity = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
            }

        }
        else
        {
            rb.gravityScale = 9.8f;
            clickedOn = false;
        }
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg + zOffset;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
