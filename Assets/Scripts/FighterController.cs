using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float animTransitionSpeed, maxSpeed, speed, crouchedSpeed, crouchedMaxSpeed, jumpPower;
    public bool isGrounded, playerControlled;
    Vector2 input;

    void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(playerControlled && isGrounded) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        anim.SetFloat("X", Mathf.Lerp(anim.GetFloat("X"), input.x, animTransitionSpeed));
        anim.SetFloat("Y", input.y);
        if (Input.GetKeyDown(KeyCode.Space)) input.y = 1;
        
        if (isGrounded)
        {
            anim.SetFloat("JumpAnim", 0);
            if (input.y == -1)
            {
                rb.velocity += new Vector2(input.x * crouchedSpeed, 0);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -crouchedMaxSpeed, crouchedMaxSpeed), rb.velocity.y);

            }
            else
            {
                rb.velocity += new Vector2(input.x * speed, 0);
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
            }
            if (input.y == 1)
            {
                isGrounded = false;
            }
        }
        

    }
}
