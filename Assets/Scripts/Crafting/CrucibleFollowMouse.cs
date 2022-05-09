using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrucibleFollowMouse : FollowMouse
{
    public Crucible crucible;
    public float maxAngle;
    public float currentAngle;
    bool itemMelting, startMelting;
    GameObject theItemMelting;
    public Image fill, pour;
    Vector2 originalScale;
    public float meltSpeed;
    internal override void Start()
    {
        start = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Item item))
        {
            if(crucible.material == 0 && crucible.item == null && crucible.mould.item == null)
            {
                crucible.item = item.item;
                crucible.mould.item = item.item;
                theItemMelting = item.gameObject;
                originalScale = item.transform.localScale;
                itemMelting = true;
                startMelting = true;
            }
            else if(crucible.item == item.item && crucible.mould.item == item.item)
            {
                crucible.item = item.item; 
                crucible.mould.item = item.item;
                theItemMelting = item.gameObject;
                originalScale = item.transform.localScale;
                itemMelting = true;
                startMelting = true;
            }
            
        }
    }
    void FixedUpdate()
    {
        if (itemMelting)
        {
            if (startMelting) 
            { 
                originalScale = theItemMelting.transform.localScale;
                startMelting = false;
            }
            var itemColor = crucible.item.item.color.ToUnityColour();
            fill.color = Color.Lerp(fill.color, itemColor, 0.1f);
            pour.color = Color.Lerp(pour.color, itemColor, 0.1f);
            theItemMelting.transform.localScale -= (Vector3)originalScale / meltSpeed;
            crucible.material += 5 / meltSpeed;
            if (theItemMelting.transform.localScale.x <= 0.01)
            {
                Destroy(theItemMelting);
                itemMelting = false;
            }
        }
    }
    internal override void Update()
    {
        Vector3 targ = origin.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        if (Input.GetMouseButtonDown(0))
        {
            if (!clickedOn)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.transform) if (hit.transform.gameObject == gameObject) clickedOn = true;
            }
            

        }
        if(clickedOn)
        {
            rb.gravityScale = 0;
            rb.velocity = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
            if (Input.GetMouseButtonUp(0)) clickedOn = false;
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
