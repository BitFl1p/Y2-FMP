using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : FollowMouse
{
    public ItemObject item;
    #region Item Hovering
    void OnMouseEnter()
    {
        ShowStats.instance.origin = gameObject;
        ShowStats.instance.item = item;
    }
    private void OnMouseExit()
    {
        ShowStats.instance.origin = null;
        ShowStats.instance.item = null;
    }
    #endregion
    public Item itemPrefab;
    public void SetItem(ItemObject item)
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
        PlayerData.instance.itemSFX[Random.Range(0, PlayerData.instance.itemSFX.Count - 1)].Play();
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
                        name = $"{item.item.itemName.Split(' ')[0]} Sword";
                        instance.SetItem(Object.Instantiate(Resources.Load<ItemObject>(name)));
                        instance.item.item.itemWeight = item.item.itemWeight;

                    }
                    else
                    {
                        name = $"{otherItem.item.item.itemName.Split(' ')[0]} Sword";
                        instance.SetItem(Instantiate(Resources.Load<ItemObject>(name)));
                        instance.item.item.itemWeight = otherItem.item.item.itemWeight;
                    }
                    
                    instance.item.item.damage = item.item.damage * otherItem.item.item.damage;
                    instance.item.item.speed = item.item.speed * otherItem.item.item.speed;
                    Destroy(otherItem.gameObject);
                    Destroy(gameObject);
                    
                }
            }
        }
    }
}