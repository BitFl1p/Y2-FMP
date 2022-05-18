using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    public float animTransitionSpeed, maxSpeed, speed, crouchedSpeed, crouchedMaxSpeed, jumpPower, drag;
    public bool isGrounded, playerControlled;
    public Vector2 input;
    public FighterController otherPlayer;
    [HideInInspector] public int side;

    public int attack;
    public Vector2 Drag(Vector2 vector, float drag)
    {
        if (vector.x > drag) vector.x -= drag;
        else if (vector.x < -drag) vector.x += drag;
        if (vector.x < drag * 1.2 && vector.x > -drag * 1.2)
        {
            vector.x = 0;
            return vector;
        }
        return vector;
    }
    void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if(playerControlled) anim.speed = PlayerData.instance.equipped.weaponNumber > 0 ? PlayerData.instance.equipped.speed : 1;
        }
        else
        {
            if (playerControlled) anim.speed = 1;
        }
        anim.transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(anim.transform.eulerAngles.y, (side + 1) * 90, 0.1f), 0);
        anim.SetFloat("Attack", attack);
        if (isGrounded) rb.velocity = Drag(rb.velocity, drag);
        if (isGrounded || attack > 0) anim.SetFloat("JumpAnim", 0);
        if (attack > 0) return;

        if (otherPlayer.transform.position.x - transform.position.x > 0) side = 1; 
        else side = -1;
        if (playerControlled)
        {
            if (isGrounded) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) input.y = 1;
        }
        
        if (isGrounded && !anim.GetBool("Stagger"))
        {

            
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
        
        anim.SetFloat("X", input.x * side);
        anim.SetFloat("Y", input.y);
        if (Input.GetKeyDown(PlayerData.instance.controls.punch) && playerControlled) Attack();
    }
    void Attack()
    {
        var state = anim.GetCurrentAnimatorStateInfo(0);
        if (!state.IsName("Attack")) 
        { 
            attack++;
        }
    }
}
