using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SwordCheck : MonoBehaviour
{
    bool swordHere;
    public GameObject button;
    public Tilemap sword;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0);
        button.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<Tilemap>())
        {
            swordHere = true;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            button.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Tilemap>() && swordHere)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
            button.SetActive(true);
        }
    }
    public void GiveBlade()
    {
        float consistency = 0;
        if (sword)
        {
            float total = 0;
            float count = 0;
            float maxHeight = -100;
            for (int x = sword.cellBounds.min.x; x <= sword.cellBounds.max.x; x++)
            {
                for (int y = sword.cellBounds.max.y; y >= sword.cellBounds.min.y; y--)
                {
                    if (sword.GetTile(new Vector3Int(x,y,0)) != null)
                    {
                        if (y > maxHeight) maxHeight = y;
                        total += y;
                        count++;
                        break;
                    } 
                }
            }
            consistency = 1 - Mathf.Abs(maxHeight - (count / total));
        }

        Debug.Log(consistency);
    }
}
