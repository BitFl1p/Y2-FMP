using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sword : MonoBehaviour
{
    Tilemap tilemap;
    float hitCount;
    public float maxHitCount;
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void Update()
    {
        if(hitCount > 0) hitCount -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out FollowMouse hammer) && hitCount <= 0)
        {
            if(hammer.GetComponent<Rigidbody2D>().gravityScale == 0) 
            { 
                hitCount = maxHitCount;
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2Int tilePos = (Vector2Int)tilemap.WorldToCell(contact.point);
                    List<Vector2Int> tilesToRemove = new List<Vector2Int> { tilePos };
                    switch (true) //the famous patented Fuck You Switch�
                    {
                        case true when contact.relativeVelocity.magnitude <= 30:
                            return;
                        case true when contact.relativeVelocity.magnitude <= 40:
                            for (int i = -2; i <= 2; i++) tilesToRemove.Add(tilePos + new Vector2Int(i, 0));
                            break;
                        case true when contact.relativeVelocity.magnitude <= 50:
                            for (int i = -3; i <= 3; i++) tilesToRemove.Add(tilePos + new Vector2Int(i, 0));
                            break;
                        case true when contact.relativeVelocity.magnitude <= 60:
                            for (int i = -4; i <= 4; i++) tilesToRemove.Add(tilePos + new Vector2Int(i, 0));
                            break;
                        case true:
                            for (int i = -5; i <= 5; i++) tilesToRemove.Add(tilePos + new Vector2Int(i, 0));
                            break;
                    }
                    PlayerData.instance.hammerSFX[Random.Range(0, PlayerData.instance.hammerSFX.Count - 1)].Play();
                    foreach (Vector3Int tile in tilesToRemove)
                    {
                        if (tilemap.HasTile(tile) && !tilemap.HasTile(tile + new Vector3Int(0,1,0))) tilemap.SetTile(tile, null);
                    }
                }
            }
        }
    }
}
