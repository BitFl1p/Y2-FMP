using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Metal,
    Blade,
    Weapon,
    Hilt
}
[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class ItemObject : ScriptableObject
{
    public ItemObject(ItemStruct item)
    {
        this.item = item;
        sprite = Resources.Load<Sprite>(item.spriteName);
    }
    public Sprite sprite;
    public ItemStruct item;
}
[System.Serializable]
public struct ItemStruct
{
    public string itemName;
    public ItemType itemType;
    public int basePrice;
    public int[] statBoosts;
    public float materialAmount; 
    public string spriteName;
    public Color color;

    [System.Serializable]
    public struct Color
    {
        public byte r, g, b, a;
    }
    public static bool operator==(ItemStruct first, ItemStruct second)
    {
        return first.itemName == second.itemName;
    }
    public static bool operator !=(ItemStruct first, ItemStruct second)
    {
        return first.itemName != second.itemName;
    }
}