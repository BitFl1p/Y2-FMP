using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class SaveSlot : MonoBehaviour
{
    public SaveManager.SaveData saveData;
    public string slotName;
    public TMP_Text nameText, timeText;
    public bool loadMainScene;

    void Update()
    {
        nameText.text = $"{slotName}:";
        var time = TimeSpan.FromSeconds(saveData.playTime);
        timeText.text = $"{time.Hours}h:{time.Minutes}m:{time.Seconds}s:{time.Milliseconds}ms";
    }
    public void LoadSave()
    {
        saveData.LoadSave();
        SceneManager.LoadScene(1);
        PlayerData.instance.UpdateInventory();
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
        PlayerData.instance.paused = false;
    }
    public void Overwrite()
    {
        saveData = PlayerData.saveManager.OverwriteSave(slotName == "AutoSave" ? 0 : (int)char.GetNumericValue(slotName[slotName.Length - 1]));
    }
    public void DeleteSave()
    {
        PlayerData.saveManager.DeleteSave(saveData,slotName);
    }
}
