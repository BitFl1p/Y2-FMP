using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAndShop : MonoBehaviour
{
    public GameObject shop, arena;

    public void EnableShop() => shop.SetActive(true);
    public void EnableArena() { }//=> arena.SetActive(true);
}
