using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Item itemPrefab;
    public ItemObject item;
    public ShopItemBuyer manager;
    public TMP_Text nameText, priceText;
    public Image sprite;
    public void Start()
    {
        if (!item) return;
        priceText.text = (item.basePrice + manager.modifier).ToString();
        nameText.text = item.itemName;
        sprite.sprite = item.sprite;
    }
    public void BuyItem()
    {
        int price = item.basePrice + manager.modifier;
        if (price <= PlayerData.instance.money)
        {
            Instantiate(itemPrefab, transform.position, transform.rotation).Instantiate(item);
            PlayerData.instance.money -= price;
        }
    }
}
