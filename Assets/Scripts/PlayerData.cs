using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    #region Singleton Shit
    public static PlayerData instance;

    public static SaveManager saveManager;
    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; 
            DontDestroyOnLoad(instance);
            saveManager = new SaveManager();
            saveManager.LookForSaves();
            
        }
        
    }
    #endregion
    public List<(ItemStruct item, int amount)> inventory = new List<(ItemStruct, int)> { };
    public ItemStruct equipped;
    public int money;
    public TMP_Text moneyText;
    public int matchesDone;
    public float playTime;
    public Controls controls = new Controls(KeyCode.C);

    public InventoryItem uiEquipped;
    public Item itemPrefab;
    public List<InventoryItem> uiItems;
    public InventoryItem uiItemPrefab;
    public Transform inventoryPlace;
    public GameObject forge, fight, pause;
    public bool fighting;
    public bool paused;
    public bool menu;
    public DialogueManager dMan;
    public Fight currentFight;
    public bool playerWon, playerLost;
    public void Clear()
    {
        inventory = new List<(ItemStruct, int)> { };
        equipped = Resources.Load<ItemObject>("NULL").item;
        money = 0;
        matchesDone = 0;
        playTime = 0;
    }
    public IEnumerator LoadFightScene()
    {
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        menu = true;
        fighting = false;
        paused = false;
        SceneManager.LoadSceneAsync(4);
        yield return null;
    }
    void Update()
    {
        
        
        
        if (menu)
        {
            forge.SetActive(false);
            fight.SetActive(false);
            return;
        }
        playTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape)) paused = !paused;
        if (paused)
        {
            forge.SetActive(false);
            fight.SetActive(false); 
            pause.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        else
        {
            pause.SetActive(false);
        }
        moneyText.text = $"{money}";
        if (fighting)
        {
            
            forge.SetActive(false);
            if (playerLost || playerWon)
            {
                StartCoroutine(LoadFightScene());
            }
            else
            {
                fight.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            forge.SetActive(true);
            fight.SetActive(false);
        }
    }
    public void UpdateInventory()
    {

        foreach (InventoryItem uiItem in uiItems) if(uiItem) Destroy(uiItem.gameObject);
        uiItems.Clear();
        int count = 0;
        foreach (var item in inventory)
        {
            count++;
            var something = Instantiate(uiItemPrefab, inventoryPlace);
            uiItems.Add(something);
            var itemToAdd = Instantiate(Resources.Load<ItemObject>(item.item.itemName));
            itemToAdd.item = item.item;
            something.Instantiate(itemToAdd, item.amount);
            if (count % 2 == 1)
            {
                something.transform.localPosition = new Vector3(-60, (150 -(count - 1) * 60), 0);
            }
            else
            {
                something.transform.localPosition = new Vector3(60, (150 - (count - 2) * 60), 0);
            }

        }
        if (equipped.weaponNumber != 0) uiEquipped.item = Resources.Load<ItemObject>(equipped.itemName);
    }
    public bool EquipItem(ItemObject item)
    {
        if (item.item.itemType == ItemType.Weapon)
        {
            if (equipped.weaponNumber != 0)
            {
                var itemInstance = Instantiate(itemPrefab, (Vector2)uiEquipped.transform.position, uiEquipped.transform.rotation);
                itemInstance.SetItem(Instantiate(Resources.Load<ItemObject>(equipped.itemName)));
                itemInstance.item.item = equipped;
                itemInstance.clickedOn = true;
            }
            equipped = item.item;
            UpdateInventory();
            return true;
        }
        return false;
    }
    
    public void AddItem(ItemObject item, int amount)
    {
        int index = inventory.FindIndex(x => x.item == item.item);
        if(index != -1) inventory[index] = (item.item, inventory[index].amount + amount);
        else inventory.Add((item.item, amount));
        UpdateInventory();
    }
    public bool RemoveItem(ItemObject item, int amount)
    {
        int index = inventory.FindIndex(x => x.item == item.item);
        if (index != -1)
        {
            if (inventory[index].amount >= amount)
            {
                inventory[index] = (item.item, inventory[index].amount - amount);
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
                if (item.transform.position.y < 2.7f) AddItem(Instantiate(item.item), 1);
                else if (!EquipItem(item.item)) AddItem(Instantiate(item.item), 1);
                Destroy(item.gameObject);
            }
        }
    }
}
public class SaveManager
{
    
    public List<SaveData> saves = new List<SaveData> { };
    // save on index 0 is always AutoSave
    // any further indexes are manual if implemented
    public void AutoSave()
    {
        if (saves.Count >= 1) 
            saves[0] = new SaveData(
                PlayerData.instance.inventory, 
                PlayerData.instance.equipped, 
                PlayerData.instance.money, 
                PlayerData.instance.matchesDone, 
                PlayerData.instance.playTime, 
                PlayerData.instance.controls);
        else 
            saves.Add(new SaveData(
                PlayerData.instance.inventory, 
                PlayerData.instance.equipped,
                PlayerData.instance.money, 
                PlayerData.instance.matchesDone, 
                PlayerData.instance.playTime, 
                PlayerData.instance.controls));
        saves[0].Save("AutoSave");
    }
    public void LookForSaves()
    {
        saves.Clear();
        if (LocateSave("AutoSave", out SaveData data))
        {
            saves.Add(data);
        }
        for ( int i = 1; ; i++ )
            if(LocateSave($"SaveSlot{i}", out data)) 
                saves.Add(data);
            else return;
    }
    public void ManualSave()
    {
        var currentSave = new SaveData(PlayerData.instance.inventory, PlayerData.instance.equipped, PlayerData.instance.money, PlayerData.instance.matchesDone, PlayerData.instance.playTime, PlayerData.instance.controls);
        saves.Add(currentSave);
        currentSave.Save($"SaveSlot{saves.IndexOf(currentSave)}");
    }
    public void UpdateSaves()
    {
        var di = new DirectoryInfo(Application.persistentDataPath);
        foreach(var file in di.GetFiles())
        {
            file.Delete();
        }
        //saves[0].Save("AutoSave");
        foreach(SaveData save in saves)
        {
            if (saves.IndexOf(save) != 0) save.Save($"SaveSlot{saves.IndexOf(save)}");
        }
    }
    public SaveData OverwriteSave(int index)
    {
        saves[index] = new SaveData(PlayerData.instance.inventory, PlayerData.instance.equipped, PlayerData.instance.money, PlayerData.instance.matchesDone, PlayerData.instance.playTime, PlayerData.instance.controls);
        saves[index].Save(index ==  0 ? "Autosave" : $"SaveSlot{index}");
        return saves[index];
    }
    public void LoadSave(int index)
    {
        if(saves.Count > index)
        {
            PlayerData.instance.inventory = saves[index].inventory;
            PlayerData.instance.money = saves[index].money;
            PlayerData.instance.matchesDone = saves[index].matchesDone;
            PlayerData.instance.controls = saves[index].controls;
        }
    }
    public void DeleteSave(SaveData save, string fileName)
    {
        saves.Remove(save);
        save.DeleteSave(fileName);
        UpdateSaves();
        LookForSaves();
    }
    bool LocateSave(string name, out SaveData data)
    {
        if (File.Exists(Application.persistentDataPath + $"/{name}.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/{name}.dat", FileMode.Open);
            data = (SaveData)bf.Deserialize(file); 
            file.Close();
            return true;
        }
        else
        {
            data = null;
            return false;
        }
    }
    [Serializable]
    public class SaveData
    {
        public List<(ItemStruct item, int amount)> inventory = new List<(ItemStruct, int)> { };
        public ItemStruct equipped;
        public int money;
        public int matchesDone; 
        public float playTime;
        public Controls controls = new Controls(KeyCode.C);
        
        public SaveData(List<(ItemStruct item, int amount)> inventory, ItemStruct equipped, int money, int matchesDone, float playTime, Controls controls)
        {
            this.inventory = inventory;
            this.equipped = equipped;
            this.money = money;
            this.matchesDone = matchesDone;
            this.playTime = playTime;
            this.controls = controls;
        }
        public void LoadSave()
        {
            PlayerData.instance.inventory = inventory;
            PlayerData.instance.UpdateInventory();
            PlayerData.instance.money = money;
            PlayerData.instance.matchesDone = matchesDone;
            PlayerData.instance.playTime = playTime;
            PlayerData.instance.controls = controls;
        }
        public void Save(string name)
        {
            DeleteSave(name);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + $"/{name}.dat");
            bf.Serialize(file, this);
            file.Close();
        }

        public void DeleteSave(string name)
        {
            if (File.Exists(Application.persistentDataPath + $"/{name}.dat"))
                File.Delete(Application.persistentDataPath + $"/{name}.dat");
        }
        
    }
}
[Serializable]
public class Controls
{
    public KeyCode punch;

    public Controls(KeyCode punch)
    {
        this.punch = punch;
    }
}