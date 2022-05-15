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
        StartCoroutine(LoadSaveCoroutine());
    }
    public IEnumerator LoadSaveCoroutine()
    {
        SceneManager.LoadSceneAsync(3); 
        if(SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded)  SceneManager.UnloadSceneAsync(2);
        saveData.LoadSave();
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
        PlayerData.instance.paused = false;
        var load = SceneManager.LoadSceneAsync(1);
        while (load.progress != 1)
        {
            yield return null;
        }
        PlayerData.instance.UpdateInventory();
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded)  SceneManager.UnloadSceneAsync(3); 
        yield return null;
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
