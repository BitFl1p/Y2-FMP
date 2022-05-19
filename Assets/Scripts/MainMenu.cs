using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<SaveSlot> saveSlots;
    public SaveSlot saveSlotPrefab;
    public RectTransform saveParent, scroll;
    public GameObject saveMenu, mainMenu;
    public Dialogue newGameDialogue;
    public void ResumeGame()
    {
        PlayerData.instance.paused = false;
    }
    public void QuitToMenu()
    {
        StartCoroutine(QuitToMenuCoroutine());
    }
    public IEnumerator QuitToMenuCoroutine()
    {
        SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        PlayerData.saveManager.AutoSave(); 
        PlayerData.instance.paused = false;
        PlayerData.instance.menu = true;
        PlayerData.instance.fighting = false;
        var load = SceneManager.LoadSceneAsync(0); 
        while (load.progress != 1)
        {
            yield return null;
        }
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) SceneManager.UnloadSceneAsync(3);
        yield return null;


    }
    public void QuitGame()
    {
        PlayerData.saveManager.AutoSave();
        Application.Quit();
    }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    public IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        PlayerData.instance.dMan.StartDialogue(newGameDialogue);
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
        PlayerData.instance.paused = false;
        var load = SceneManager.LoadSceneAsync(1);
        while (load.progress != 1)
        {
            yield return null;
        }
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) SceneManager.UnloadSceneAsync(3);
        yield return null;
    }
    public void GoToSaves()
    {
        saveMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void GoToMain()
    {
        saveMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void LoadSave(int index)
    {
        StartCoroutine(LoadSaveCoroutine(index));
    }
    public IEnumerator LoadSaveCoroutine(int index)
    {
        SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        PlayerData.saveManager.LoadSave(index); 
        var load = SceneManager.LoadSceneAsync(1);
        while (load.progress != 1)
        {
            yield return null;
        }
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
        PlayerData.instance.paused = false;
        PlayerData.instance.UpdateInventory();
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) SceneManager.UnloadSceneAsync(3);
        yield return null;
    }
    public void MakeSave()
    {
        PlayerData.saveManager.ManualSave();
    }

    void OnGUI()
    {
        scroll.localPosition = new Vector2(0, Mathf.Clamp(scroll.localPosition.y + Input.mouseScrollDelta.y * -20, 0, Mathf.Clamp((saveSlots.Count - 7) * 100, 0, Mathf.Infinity)));
        foreach (var save in PlayerData.saveManager.saves)
        {
            bool exists = false;
            foreach (var saveSlot in saveSlots)
            {

                if (saveSlot.saveData == save)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                var slot = Instantiate(saveSlotPrefab, scroll.transform);
                saveSlots.Add(slot);
                int index = PlayerData.saveManager.saves.IndexOf(save);
                slot.slotName = index == 0 ? "AutoSave" : $"SaveSlot{index}";
                slot.transform.localPosition = new Vector2(0, saveParent.rect.height/2 - (index * 100) - 45);
                slot.saveData = save;
            }
        }
        foreach (var saveSlot in saveSlots)
        {

            if (saveSlot) if (!PlayerData.saveManager.saves.Contains(saveSlot.saveData))
            {
                Destroy(saveSlot.gameObject);
            }
        }
        saveSlots.RemoveAll(item => item == null);
    }
}
