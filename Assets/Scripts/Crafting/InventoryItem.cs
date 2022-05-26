using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public Image sprite;
    public Sprite defaultSprite;
    public TMP_Text nameText;
    public ItemObject item;
    public Item itemPrefab;
    public int amount;
    public bool equipped;
    public void Instantiate(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(equipped) SpawnItemEquipped();
        else SpawnItem();
    }

    public void SpawnItem()
    {
        if(PlayerData.instance.RemoveItem(item, 1))
        {
            var bruh = Instantiate(itemPrefab, (Vector2)transform.position, transform.rotation);
            bruh.SetItem(Instantiate(item));
            bruh.clickedOn = true;
        }
    }
    public void SpawnItemEquipped()
    {
        if (PlayerData.instance.equipped.itemType == ItemType.Weapon)
        {
            var bruh = Instantiate(itemPrefab, (Vector2)transform.position, transform.rotation);
            bruh.SetItem(Instantiate(Resources.Load<ItemObject>(PlayerData.instance.equipped.itemName)));
            bruh.item.item = PlayerData.instance.equipped;
            bruh.clickedOn = true;
            item = Resources.Load<ItemObject>("NULL");
            PlayerData.instance.equipped = Resources.Load<ItemObject>("NULL").item;
            PlayerData.instance.UpdateInventory();
            sprite.sprite = defaultSprite;
        }
    }
    private void Update()
    {
        if (equipped)
        {
            if (item.item.itemType == ItemType.Weapon)
            {
                nameText.text = $"{(item.item.itemWeight != ItemWeight.NA ? item.item.itemWeight.ToString() : "")} {item.item.itemName}";
                sprite.sprite = item.sprite;
            }
            else
            {
                nameText.text = "None";
                sprite.sprite = defaultSprite;
            }
        }
        else
        {
            nameText.text = $"{(item.item.itemWeight != ItemWeight.NA ? item.item.itemWeight.ToString() : "")} {item.item.itemName} : {amount}";
            sprite.sprite = item.sprite;
            if (amount <= 0) Destroy(gameObject);
        }
    }
}
