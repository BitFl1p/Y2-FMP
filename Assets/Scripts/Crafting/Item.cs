using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : FollowMouse
{
    public ItemObject item;
    public Item itemPrefab;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (clickedOn)
        {
            if (collision.gameObject.TryGetComponent(out Item otherItem))
            {

                if (item.item.itemType == ItemType.Blade && otherItem.item.item.itemType == ItemType.Hilt || item.item.itemType == ItemType.Hilt && otherItem.item.item.itemType == ItemType.Blade)
                {
                    var instance = Instantiate(itemPrefab);
                    string name;
                    if (item.item.itemType == ItemType.Blade)
                    {
                        instance.item.item.itemWeight = item.item.itemWeight;
                        name = $"{item.item.itemName.Split(' ')[0]} Sword";
                    }
                    else
                    {
                        instance.item.item.itemWeight = otherItem.item.item.itemWeight;
                        name = $"{otherItem.item.item.itemName.Split(' ')[0]} Sword";
                    }
                    instance.Instantiate(Object.Instantiate(Resources.Load<ItemObject>(name)));
                    instance.item.item.damage = item.item.damage * otherItem.item.item.damage;
                    instance.item.item.speed = item.item.speed * otherItem.item.item.speed;
                    
                }
            }
        }
    }
}