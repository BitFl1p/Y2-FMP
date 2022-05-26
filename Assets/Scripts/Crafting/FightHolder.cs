using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            var thing = PlayerData.instance.dMan.dialogueText.transform.parent.gameObject;
            while (PlayerData.instance.dMan.GetComponent<Animator>().GetBool("Dialoguing") || thing.activeSelf)
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
        PlayerData.instance.fight.GetComponent<FightManager>().timer = PlayerData.instance.fight.GetComponent<FightManager>().maxTimer;
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
