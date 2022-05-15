using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public TMP_Text winLose, earnings;
    public void Return()
    {
        StartCoroutine(ReturnCoroutine());
    }
    private void Update()
    {
        if (PlayerData.instance.playerWon)
        {
            winLose.text = "You Win!";
            earnings.text = $"You earned:\n{PlayerData.instance.currentFight.reward}";
        }
        else if(PlayerData.instance.playerLost)
        {
            winLose.text = "You Lost..."; 
            //earnings.text = "You earned:\nnothing, dumbass, you lost.";
            earnings.text = "";
        }
    }
    public IEnumerator ReturnCoroutine()
    {
        SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = false;
        PlayerData.instance.paused = false;
        if (PlayerData.instance.playerWon) 
        { 
            PlayerData.instance.money += PlayerData.instance.currentFight.reward;
            if(PlayerData.instance.currentFight.fightNumber > PlayerData.instance.matchesDone) PlayerData.instance.matchesDone = PlayerData.instance.currentFight.fightNumber; 
        }
        PlayerData.instance.playerWon = false;
        PlayerData.instance.playerLost = false;
        PlayerData.saveManager.AutoSave();
        var load = SceneManager.LoadSceneAsync(1);
        while (load.progress != 1)
        {
            yield return null;
        }
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) SceneManager.UnloadSceneAsync(3);
        yield return null;
    }
}
