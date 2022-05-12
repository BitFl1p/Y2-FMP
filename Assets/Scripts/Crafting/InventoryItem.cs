using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public Image sprite;
    public TMP_Text nameText;
    public ItemObject item;
    public Item itemPrefab;
    public int amount;
    
    public void Instantiate(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpawnItem();
    }

    public void SpawnItem()
    {
        if(PlayerData.instance.RemoveItem(item, 1))
        {
            var bruh = Instantiate(itemPrefab, (Vector2)transform.position, transform.rotation);
            bruh.Instantiate(Instantiate(item));
            bruh.clickedOn = true;
        }
    }
    private void Update()
    {
        nameText.text = $"{(item.item.itemWeight != ItemWeight.NA ? item.item.itemWeight.ToString() : "")} {item.item.itemName} : {amount}";
        sprite.sprite = item.sprite;
        if (amount <= 0) Destroy(gameObject);
    }
}
