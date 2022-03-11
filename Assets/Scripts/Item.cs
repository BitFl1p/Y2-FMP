using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : FollowMouse
{
    public ItemObject item;
    public void Instantiate(ItemObject item)
    {
        this.item = item;
        OnEnable();
    }
    private void OnEnable()
    {
        if (!item) return;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
    internal override void Update()
    {
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
                rb.gravityScale = 0f;
                rb.velocity = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
            }

        }
        else
        {
            rb.gravityScale = 9.8f;
            clickedOn = false;
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            //horizontal movement
        }
        else if(Input.GetAxisRaw("Vertical") != 0)
        {
            //vertical movement
        }
    }
}