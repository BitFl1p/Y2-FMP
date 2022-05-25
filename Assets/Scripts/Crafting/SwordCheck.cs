using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SwordCheck : MonoBehaviour
{
    bool swordHere;
    public GameObject button;
    public Tilemap sword;
    public Item itemPrefab;
    public ItemObject swordMaterial;
    public int desiredHeight;
    public ItemWeight bladeWeight;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0);
        button.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<Tilemap>())
        {
            swordHere = true;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            button.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Tilemap>() && swordHere)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            button.SetActive(true);
        }
    }
    public void GiveBlade()
    {
        float consistency = 0;float total = 0;
        float count = 0;
        float maxHeight = -100;
        for (int x = sword.cellBounds.min.x; x <= sword.cellBounds.max.x; x++)
        {
            for (int y = sword.cellBounds.max.y; y >= sword.cellBounds.min.y; y--)
            {
                if (sword.GetTile(new Vector3Int(x,y,0)) != null)
                {
                    if (y > maxHeight) maxHeight = y + 3;
                    total += y + 3;
                    count++;
                    break;
                } 
            }
        }
        total = Mathf.Abs(total) / desiredHeight;
        consistency = (float) System.Math.Round((total / count), 1);
        Item item = Instantiate(itemPrefab);
        item.SetItem(Instantiate(Resources.Load<ItemObject>($"{swordMaterial.item.itemName} Blade"))); 
        item.item.item.damage = swordMaterial.item.damage * consistency;
        item.item.item.speed = swordMaterial.item.speed * consistency;
        switch (bladeWeight)
        {
            case ItemWeight.Light:
                item.item.item.damage *= .5f;
                item.item.item.speed *= 2f;
                break;
            case ItemWeight.Heavy:
                item.item.item.damage *= 2f;
                item.item.item.speed *= .5f;
                break;
        }
        
        item.item.item.itemWeight = bladeWeight;
        Debug.Log(consistency);
        Destroy(transform.parent.gameObject);
    }
}
