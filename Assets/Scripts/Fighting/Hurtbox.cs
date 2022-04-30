using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    SpriteRenderer sprite;
    float originalZ; 
    FighterController player;
    float timer;
    void Start()
    {
        player = transform.parent.parent.parent.GetComponent<FighterController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalZ = sprite.transform.position.z;
    }
    void OnEnable()
    {
        timer = 0;
    }
    void Update()
    {
        sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, originalZ);
        sprite.transform.localPosition = new Vector3(0, 0, sprite.transform.localPosition.z);
        if (timer > 0) timer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hitbox hitbox))
        {
            if (hitbox.transform.parent.parent.parent != player && timer <= 0)
            {
                Animator anim = hitbox.transform.parent.parent.GetComponent<Animator>();
                player.otherPlayer.attack = 0;
                anim.SetBool("Stagger", true);
                anim.SetFloat("StaggerPlace", (int)hitbox.bodyPart);
                timer = 1;
            }
        }
    }
}
