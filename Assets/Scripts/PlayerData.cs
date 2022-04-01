using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
    public List<(ItemObject item, int amount)> inventory = new List<(ItemObject, int)> { };
    public int money;
    public int matchesDone;
    public List<InventoryItem> uiItems;
    public InventoryItem uiItemPrefab;
    public Transform inventoryPlace;
    public GameObject forge, fight, pause;
    public bool fighting;
    public bool paused;
    [System.Serializable]
    public class Controls
    {
        public KeyCode punch;
        public KeyCode kick;
        
        public Controls(KeyCode punch, KeyCode kick)
        {
            this.punch = punch;
            this.kick = kick;
        }
    }
    public Controls controls = new Controls(KeyCode.C, KeyCode.X);
    void Update()
    {
        /*if (paused)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }*/
        if (fighting)
        {
            forge.SetActive(false);
            fight.SetActive(true);
        }
        else
        {
            forge.SetActive(true);
            fight.SetActive(false);
        }
    }
    void UpdateInventory()
    {

        foreach (InventoryItem uiItem in uiItems) if(uiItem) Destroy(uiItem.gameObject);
        uiItems.Clear();
        int count = 0;
        foreach (var item in inventory)
        {
            count++;
            var something = Instantiate(uiItemPrefab, inventoryPlace);
            uiItems.Add(something);
            something.Instantiate(item.item, item.amount);
            if (count % 2 == 1)
            {
                something.transform.localPosition = new Vector3(-60, (-(count - 1) * 80), 0);
            }
            else
            {
                something.transform.localPosition = new Vector3(60, (-(count / 2 - 1) * 80), 0);
            }

        }
    }
    
    public void AddItem(ItemObject item, int amount)
    {
        int index = inventory.FindIndex(x => x.item == item);
        if(index != -1) inventory[index] = (item, inventory[index].amount + amount);
        else inventory.Add((item, amount));
        UpdateInventory();
    }
    public bool RemoveItem(ItemObject item, int amount)
    {
        int index = inventory.FindIndex(x => x.item == item);
        if (index != -1)
        {
            if (inventory[index].amount >= amount)
            {
                inventory[index] = (item, inventory[index].amount - amount);
                if (inventory[index].amount <= 0) inventory.Remove(inventory[index]);
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
