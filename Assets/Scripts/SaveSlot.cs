using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SaveSlot : MonoBehaviour
{
    public SaveManager.SaveData saveData;
    public string slotName;
    public TMP_Text nameText, timeText;

    void Update()
    {
        nameText.text = slotName;
        var time = TimeSpan.FromSeconds(saveData.playTime);
        timeText.text = $"{time.Hours}h:{time.Minutes}m:{time.Seconds}s:{time.Milliseconds}ms";
    }
    public void LoadSave()
    {
        saveData.LoadSave();
    }
}
