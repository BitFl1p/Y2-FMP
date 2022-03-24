using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float animTransitionSpeed, maxSpeed, speed, crouchedSpeed, crouchedMaxSpeed, jumpPower;
    public bool isGrounded;
    Vector2 input;

    void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        anim.SetFloat("X", Mathf.Lerp(anim.GetFloat("X"), input.x, animTransitionSpeed));
        anim.SetFloat("Y", input.y);
        if(isGrounded && (input.y == 1 || Input.GetKeyDown(KeyCode.Space)))
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }
    void FixedUpdate()
    {
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
    }
}
