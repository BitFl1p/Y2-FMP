using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fight", menuName = "Special/Fight")]
public class Fight : ScriptableObject
{
    public int fightNumber;
    public int reward;
    public int enemyWeapon;
    public Dialogue dialogue;
    public Sprite enemySprite;
}
