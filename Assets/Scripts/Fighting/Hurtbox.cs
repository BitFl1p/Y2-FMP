using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Hurtbox : MonoBehaviour
{
    //SpriteRenderer sprite;
    float originalZ; 
    FighterController player;
    PlayerStats playerStats;
    float timer;
    public CinemachineVirtualCamera vcam;
    public GameObject hit, block;
    void Start()
    {
        player = transform.parent.parent.parent.GetComponent<FighterController>();
        playerStats = player.GetComponent<PlayerStats>();
        //sprite = GetComponentInChildren<SpriteRenderer>();
        //originalZ = sprite.transform.position.z;
    }
    void OnEnable()
    {
        timer = 0;
    }
    void Update()
    {
        //sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, originalZ);
        //sprite.transform.localPosition = new Vector3(0, 0, sprite.transform.localPosition.z);
        if (timer > 0) 
        {
            timer -= Time.unscaledDeltaTime;
        }
        if (timer > 0.9)
        {
            vcam.m_Lens.OrthographicSize = 6.4f;
            Time.timeScale = 0.1f;
        }
        else 
        {
            vcam.m_Lens.OrthographicSize = 6.5f;
            Time.timeScale = 1f; 
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hitbox hitbox))
        {
            if (hitbox.transform.parent.parent.parent != player && timer <= 0)
            {
                Animator anim = hitbox.transform.parent.parent.GetComponent<Animator>();
                var otherPlayer = anim.transform.parent.GetComponent<PlayerStats>();
                var otherPlayerController = otherPlayer.GetComponent<FighterController>();
                if (otherPlayerController.input.x == -1 * otherPlayerController.side && otherPlayerController.attack == 0)
                {
                    anim.SetBool("Block", true);
                    otherPlayerController.rb.velocity = Vector2.zero;
                    Instantiate(block, (transform.position + collision.transform.position)/2, Quaternion.identity).transform.LookAt(transform.position);
                }
                else
                {
                    switch ((int)hitbox.bodyPart)
                    {
                        case -1:
                            otherPlayer.Damage((playerStats.damage + (player.playerControlled ? PlayerData.instance.equipped.damage : PlayerData.instance.currentFight.enemyWeapon.damage)) * .8f);
                            break;
                        case 0:
                            otherPlayer.Damage(playerStats.damage + (player.playerControlled ? PlayerData.instance.equipped.damage : PlayerData.instance.currentFight.enemyWeapon.damage));
                            break;
                        case 1:
                            otherPlayer.Damage((playerStats.damage + (player.playerControlled ? PlayerData.instance.equipped.damage : PlayerData.instance.currentFight.enemyWeapon.damage)) * 1.5f);
                            break;

                    }
                    Instantiate(hit, (transform.position + collision.transform.position) / 2, Quaternion.identity).transform.LookAt(transform.position);
                    player.otherPlayer.attack = 0;
                    anim.SetBool("Stagger", true);
                    anim.SetFloat("StaggerPlace", (int)hitbox.bodyPart);
                }
                timer = 1f;
            }
        }
    }
}
