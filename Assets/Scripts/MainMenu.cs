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
    public bool pauseMenu;
    public void ResumeGame()
    {
        PlayerData.instance.paused = false;
    }
    public void QuitToMenu()
    {
        PlayerData.saveManager.AutoSave();
        SceneManager.LoadScene(0); 
        PlayerData.instance.paused = false;
        PlayerData.instance.menu = true;
        PlayerData.instance.fighting = false;
    }
    public void QuitGame()
    {
        PlayerData.saveManager.AutoSave();
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
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
        PlayerData.saveManager.LoadSave(index); 
        SceneManager.LoadScene(1);
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
                slot.loadMainScene = !pauseMenu;
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
