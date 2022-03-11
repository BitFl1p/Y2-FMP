using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemBuyer : MonoBehaviour
{
    public int modifier;
    public List<ItemObject> itemsToSell;
    public List<ShopItem> shopThings;
    public TMP_Text moneyText;

    private void Start()
    {
        foreach(ShopItem shopThing in shopThings)
        {
            shopThing.item = itemsToSell[Random.Range(0, itemsToSell.Count - 1)];
            shopThing.Start();
        }
    }
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        moneyText.text = "Money: " + PlayerData.instance.money;
    }
}
