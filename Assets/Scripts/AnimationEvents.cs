using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Jump()
    {
        FighterController fighter = GetComponentInParent<FighterController>();
        Rigidbody2D rb = fighter.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(fighter.input.x * fighter.maxSpeed * 2, fighter.jumpPower);
    }
    public void JumpHold()
    {
        anim.SetFloat("JumpAnim", 1);
    }
    public void StaggerDone()
    {
        anim.SetBool("Stagger", false);
    }
}
