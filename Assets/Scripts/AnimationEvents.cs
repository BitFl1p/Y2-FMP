using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator anim;
    public void KillYourself() => Destroy(gameObject);
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void BlockDone()
    {
        anim.SetBool("Block", false);
    }
    public void Jump()
    {
        FighterController fighter = GetComponentInParent<FighterController>();
        Rigidbody2D rb = fighter.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(fighter.input.x * fighter.maxSpeed * 2, fighter.jumpPower);
    }
    public void Velocity(float vel)
    {
        FighterController fighter = GetComponentInParent<FighterController>();
        Rigidbody2D rb = fighter.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(vel * fighter.maxSpeed * fighter.side, rb.velocity.y);
    }
    public void JumpHold()
    {
        anim.SetFloat("JumpAnim", 1);
    }
    public void JumpCancel()
    {
        GetComponentInParent<FighterController>().input.y = 0;
    }
    public void StaggerDone()
    {
        anim.SetBool("Stagger", false);
    }
    public void AttackDone()
    {
        var player = GetComponentInParent<FighterController>();
        player.input = Vector2.zero;
        player.isGrounded = true;
        player.attack = 0;
    }
    public void SetCharacterAnim(int index)
    {
        anim.SetInteger("Animation",index);
    }
    public void DestroyCharacter()
    {
        Destroy(transform.parent.gameObject);
    }
}
