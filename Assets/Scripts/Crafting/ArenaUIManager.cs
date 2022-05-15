using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    public List<Button> fights;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < fights.Count; i++)
        {
            fights[i].GetComponentInChildren<TMP_Text>().text = $"{i + 1}";
            if (i <= PlayerData.instance.matchesDone) fights[i].interactable = true;
            else fights[i].interactable = false;
        }
    }
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
}
