using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public List<SaveSlot> saveSlots;
    public SaveSlot saveSlotPrefab;
    public void QuitGame()
    {
        PlayerData.saveManager.AutoSave();
    }
    public void LoadSave(int index)
    {
        PlayerData.saveManager.LoadSave(index);
    }
    void Update()
    {
        foreach (var save in PlayerData.saveManager.saves)
        {
            bool exists = false;
            foreach (var saveSlot in saveSlots)
            {

                if (saveSlot.saveData.playTime == save.playTime)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                var slot = Instantiate(saveSlotPrefab, transform);
                saveSlots.Add(slot);
                int index = PlayerData.saveManager.saves.IndexOf(save);
                slot.slotName = index == 0 ? "AutoSave" : $"SaveSlot{index}";
                slot.saveData = save;
            }
        }
    }
}
