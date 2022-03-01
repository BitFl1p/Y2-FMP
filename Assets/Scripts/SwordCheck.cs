using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwordCheck : MonoBehaviour
{
    bool swordHere;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<Tilemap>())
        {
            swordHere = true;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Tilemap>() && swordHere)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
        }
    }
}
