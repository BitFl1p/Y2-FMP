using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemBuyer : MonoBehaviour
{
    public TMP_Text refreshButtonText;
    public int modifier;
    public List<ItemObject> itemsToSell;
    public List<ShopItem> shopThings;

    private void Start()
    {
        Refresh();
    }
    public void PayToRefresh()
    {
        if (PlayerData.instance.money >= modifier) 
        {
            PlayerData.instance.money -= modifier;
            Refresh(); 
        }
    }
    public void Refresh()
    {
        modifier = Random.Range(0,11);
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
        refreshButtonText.text = $"Refresh Shop: {modifier}";
    }
}
