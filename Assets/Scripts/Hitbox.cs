using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    SpriteRenderer sprite;
    float originalZ;
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalZ = sprite.transform.position.z;
    }
    void Update()
    {
        sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, originalZ);
        sprite.transform.localPosition = new Vector3(0, 0, sprite.transform.localPosition.z);
    }
}
