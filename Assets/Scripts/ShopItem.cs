using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public ItemObject item;
    public ShopItemBuyer manager;
    public TMP_Text nameText, priceText;
    public Image sprite;
    private void Start()
    {
        priceText.text = (item.basePrice + manager.modifier).ToString();
        nameText.text = item.itemName;
        sprite.sprite = item.sprite;
    }
}
