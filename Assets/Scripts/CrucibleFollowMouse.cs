using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucibleFollowMouse : FollowMouse
{
    public Crucible crucible;
    public float maxAngle;
    public float currentAngle;
    internal override void Start()
    {
        start = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    internal override void Update()
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
                if (hit.transform) if (hit.transform.gameObject == gameObject) clickedOn = true;
            }
            else
            {
                rb.gravityScale = 0;
                rb.velocity = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
            }

        }
        else
        {
            rb.velocity = (start - (Vector2)transform.position).normalized * Vector2.Distance(start, transform.position) * moveSpeed / 2;
            clickedOn = false;
        }
        currentAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg + zOffset;
        //angle = Mathf.Clamp(angle, -90, 0);
        if (!clickedOn) transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, 0, 0.1f)));
        else 
        {
            maxAngle = 270 + (crucible.material / crucible.maxMaterial) * 80f;
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, currentAngle, 0.1f));
            if (transform.eulerAngles.z < 180 || transform.eulerAngles.z > 350) transform.eulerAngles = new Vector3(0, 0, 350); 
            else if (transform.eulerAngles.z < maxAngle) transform.eulerAngles = new Vector3(0, 0, maxAngle);
            //transform.eulerAngles = new Vector3(0, 0, currentAngle);
        }
    }
}
