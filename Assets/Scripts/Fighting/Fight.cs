using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fight", menuName = "Special/Fight")]
public class Fight : ScriptableObject
{
    public float difficulty;
    public int fightNumber;
    public int reward;
    public ItemStruct enemyWeapon;
    public Dialogue dialogue;
    public Sprite enemySprite;
}
