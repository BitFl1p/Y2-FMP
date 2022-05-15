using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health, maxHealth;
    public int damage;
    public Slider healthSlider;
    public List<GameObject> weapons;
    Animator anim;
    private void Start()
    {
        foreach (var weapon in weapons) weapon.SetActive(false); 
        anim = GetComponentInChildren<Animator>();
        if (GetComponent<FighterController>().playerControlled)
        {
            healthSlider = PlayerData.instance.fight.GetComponent<FightManager>().player1Health; 
            if (PlayerData.instance.equipped.weaponNumber != 0) weapons[PlayerData.instance.equipped.weaponNumber - 1].SetActive(true); 
            anim.SetFloat("Weapon", PlayerData.instance.equipped != null ? PlayerData.instance.equipped.weaponNumber : 0);
        }
        else
        {
            if (PlayerData.instance.currentFight.enemyWeapon != 0) weapons[PlayerData.instance.currentFight.enemyWeapon - 1].SetActive(true);
            healthSlider = PlayerData.instance.fight.GetComponent<FightManager>().player2Health;
            anim.SetFloat("Weapon", PlayerData.instance.currentFight.enemyWeapon);
        }
        
        
        
        
    }
    void Update()
    {
        if(!healthSlider) 
            if (GetComponent<FighterController>().playerControlled) healthSlider = PlayerData.instance.fight.GetComponent<FightManager>().player1Health;
            else healthSlider = PlayerData.instance.fight.GetComponent<FightManager>().player2Health;
        healthSlider.value = health + 15; 
        healthSlider.maxValue = maxHealth + 15;
    }
    public void Damage(float damage)
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
