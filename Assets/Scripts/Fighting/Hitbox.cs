using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum BodyPart
    {
        Head = 1,
        Torso = 0,
        Legs = -1
    }
    Animator anim;
    public BodyPart bodyPart;
    
    float originalZ;
    void Start()
    {
        
        anim = transform.parent.parent.GetComponent<Animator>();
    }
    
}
