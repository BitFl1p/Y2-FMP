using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public Slider player1Health, player2Health;
    public TMP_Text timerText;
    public int maxTimer;
    float timer;
    private void Start()
    {
        timer = maxTimer;
    }
    private void Update()
    {
        timerText.text = ((int)timer).ToString();
        timer -= Time.deltaTime;
        if (timer <= 0) PlayerData.instance.playerLost = true;
    }
}
