using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemBuyer : MonoBehaviour
{
    public bool refresh;
    public int modifier;
    public List<ItemObject> itemsToSell;
    public List<ShopItem> shopThings;

    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        foreach(ShopItem shopThing in shopThings)
        {
            shopThing.item = itemsToSell[Random.Range(0, itemsToSell.Count)];
            shopThing.Start();
        }
    }
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (refresh)
        {
            Refresh();
            refresh = false;
        }
    }
}
