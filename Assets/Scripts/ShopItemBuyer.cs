using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemBuyer : MonoBehaviour
{
    public int modifier;
    public List<ItemObject> itemsToSell;
    public List<ShopItem> shopThings;

    private void Start()
    {
        foreach(ShopItem shopThing in shopThings)
        {
            shopThing.item = itemsToSell[Random.Range(0, itemsToSell.Count - 1)];
        }
    }
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
    public void BuyItem(int price)
    {
        price += modifier;
        if (price <= PlayerData.instance.money)
        {
            
        }
    }
    public void GetItem()
    {

    }
}
