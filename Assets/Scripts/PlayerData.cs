using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region Singleton Shit
    public static PlayerData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    #endregion
    public Dictionary<ItemObject, int> inventory = new Dictionary<ItemObject, int> { };
    public int money;
    public int matchesDone;
    public List<InventoryItem> uiItems;
    public InventoryItem uiItemPrefab;
    public Transform inventoryPlace;
    
    void UpdateInventory()
    {

        foreach (InventoryItem uiItem in uiItems)
        {
            if(uiItem) Destroy(uiItem.gameObject);
        }
        uiItems.Clear();
        foreach (var item in inventory)
        {
            var something = Instantiate(uiItemPrefab, inventoryPlace);
            uiItems.Add(something);
            something.Instantiate(item.Key, item.Value);
        }
        int count = 0;
        foreach (InventoryItem uiItem in uiItems)
        {
            count++;
            if (count % 2 == 1)
            {
                uiItem.transform.localPosition = new Vector3(10, (count * 2), 0);
            }
            else
            {
                uiItem.transform.localPosition = new Vector3(10, (count / 2 * 2), 0);
            }
        }
    }
    public void AddItem(ItemObject item, int amount)
    {
        if (inventory.ContainsKey(item)) inventory[item] += amount;
        else inventory.Add(item, amount);
        UpdateInventory();
    }
    public bool RemoveItem(ItemObject item, int amount)
    {
        if (inventory.ContainsKey(item)) 
        {
            if (inventory[item] >= amount)
            {
                inventory[item] -= amount;
                UpdateInventory();
                return true;
            }
            else return false;
        }
        else return false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Item item))
        {
            if (!item.clickedOn)
            {
                AddItem(item.item, 1);
                Destroy(item.gameObject);
            }
        }
    }
}
