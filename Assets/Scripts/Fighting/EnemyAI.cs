using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    FighterController player;
    Transform otherPlayer;
    public float tickRate;
    float tick;
    public float playerDistance;
    private void Start()
    {
        player = GetComponent<FighterController>();
        otherPlayer = player.otherPlayer.transform;
        tickRate = PlayerData.instance.currentFight.difficulty;
        //player.anim.speed = PlayerData.instance.equipped.weaponNumber > 0 ? PlayerData.instance.equipped.speed : 1;
    }

    private void Update()
    {
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            player.anim.speed = PlayerData.instance.currentFight.enemyWeapon.speed > 0 ? PlayerData.instance.currentFight.enemyWeapon.speed : 1;
        }
        else
        {
            player.anim.speed = 1;
        }
        if(Vector2.Distance(transform.position, otherPlayer.position) < playerDistance)
        {
            if(tick <= 0)
            {
                tick = tickRate;
                player.input = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                player.attack = 1;
            }
            tick -= Time.deltaTime;
        }
        else
        {
            if(tick <= 0)
            {
                tick = tickRate;
                player.input.y = Random.Range(-1, 2);
            }
            tick -= Time.deltaTime;
            player.input.x = 1 * player.side;
        }
    }
}
