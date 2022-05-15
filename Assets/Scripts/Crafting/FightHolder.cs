using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightHolder : MonoBehaviour
{
    public Fight fight;
    public void StartFight()
    {
        StartCoroutine(StartFightCoroutine());
    }
    public IEnumerator StartFightCoroutine()
    {
        if (fight.dialogue != null)
        {
            PlayerData.instance.dMan.StartDialogue(fight.dialogue);
            while (PlayerData.instance.dMan.GetComponent<Animator>().GetBool("Dialoguing"))
            {
                yield return null;
            }
        }
        PlayerData.instance.currentFight = fight;
        SceneManager.LoadSceneAsync(3);
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded) SceneManager.UnloadSceneAsync(0);
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) SceneManager.UnloadSceneAsync(1);
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded) SceneManager.UnloadSceneAsync(2);
        PlayerData.saveManager.AutoSave();
        PlayerData.instance.paused = false;
        PlayerData.instance.menu = false;
        PlayerData.instance.fighting = true;
        var load = SceneManager.LoadSceneAsync(2);
        while (load.progress != 1)
        {
            yield return null;
        }
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded) SceneManager.UnloadSceneAsync(3);
        yield return null;

        yield return null;
    }
}
