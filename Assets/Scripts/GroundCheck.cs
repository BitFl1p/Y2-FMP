using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    FighterController player;
    private void OnEnable()
    {
        player = GetComponentInParent<FighterController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground") player.isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground") player.isGrounded = false;
    }
}
