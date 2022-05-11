using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth;
    public Slider healthSlider;
    void Update()
    {
        healthSlider.maxValue = maxHealth + 20;
        healthSlider.value = health + 20;
    }
    public void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (GetComponent<FighterController>().playerControlled)
                PlayerData.instance.playerLost = true;
            else
                PlayerData.instance.playerWon = true;
        }
    }
}
