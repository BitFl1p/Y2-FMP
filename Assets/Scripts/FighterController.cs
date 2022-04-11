using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float animTransitionSpeed, maxSpeed, speed, crouchedSpeed, crouchedMaxSpeed, jumpPower;
    public bool isGrounded, playerControlled;
    public Vector2 input;
    public FighterController otherPlayer;
    int side;

    public int attack;
    float attackCooldown;
    public float attackCooldownMax;

    void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        if (otherPlayer.transform.position.x - transform.position.x > 0) side = 1; 
        else side = -1;
        if (playerControlled)
        {
            if (Input.GetKeyDown(PlayerData.instance.controls.punch)) Attack();
            if (isGrounded) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) input.y = 1;
        }
        
        if (isGrounded)
        {

            anim.SetFloat("JumpAnim", 0);
            if (input.y == -1)
            {
                rb.velocity += new Vector2(attack == 0 ? input.x * crouchedSpeed : 0, 0);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -crouchedMaxSpeed, crouchedMaxSpeed), rb.velocity.y);

            }
            else
            {
                rb.velocity += new Vector2(attack == 0 ? input.x * speed : 0, 0);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
            }
            if (input.y == 1)
            {
                isGrounded = false;
                rb.velocity += new Vector2(input.x * speed, 0);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
            }
        }
        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, (side - 1) * -90, 0.1f), 0);
        anim.SetFloat("X", Mathf.Lerp(anim.GetFloat("X"), input.x * side, animTransitionSpeed));
        anim.SetFloat("Y", input.y);
        var state = anim.GetCurrentAnimatorStateInfo(0);
        if ((state.normalizedTime > 0.7 + (attack-1) && state.IsName("Attack")) || anim.GetFloat("Attack") == 0) 
        { 
            if(anim.GetFloat("Attack") < attack)
            {
                anim.SetFloat("Attack", attack);
                attackCooldown = attackCooldownMax;
            }
            else if(attackCooldown <= 0)
            {
                attack = 0;
                anim.SetFloat("Attack", attack);
            }
        }
        attackCooldown -= Time.deltaTime;
    }
    void Attack()
    {
        var state = anim.GetCurrentAnimatorStateInfo(0);
        if (!state.IsName("Attack") || state.normalizedTime > 0.7) attack++;
    }
}
