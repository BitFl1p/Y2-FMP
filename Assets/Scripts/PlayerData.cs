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

    public Dictionary<Item, int> inventory;
    public int money;
    public int matchesDone;

    public void AddItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item)) inventory[item] += amount;
        else inventory.Add(item, amount);
    }
    public bool RemoveItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item)) 
        {
            if (inventory[item] >= amount)
            {
                inventory[item] -= amount;
                return true;
            }
            else return false;
        }
        else return false;
    }
}
