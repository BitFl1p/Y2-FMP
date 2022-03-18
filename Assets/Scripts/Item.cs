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
        
        if (clickedOn)
        {
            gameObject.layer = 8;
            rb.gravityScale = 0f;
            rb.velocity = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveSpeed;
            if (Input.GetMouseButtonUp(0)) clickedOn = false;
        }
        else
        {
            gameObject.layer = 7;
            rb.gravityScale = 9.8f;
            clickedOn = false;
        }
    }
}