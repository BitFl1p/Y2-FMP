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
public enum ItemWeight
{
    Light,
    Balanced,
    Heavy
}
[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public ItemStruct item;
}
[System.Serializable]
public struct ItemStruct
{
    public string itemName;
    public ItemType itemType;
    public ItemWeight itemWeight;
    public int basePrice;
    public float materialAmount;
    public float damage, speed;
    public float weaponNumber;
    public Color color;

    [System.Serializable]
    public struct Color
    {
        [Range(0.0f, 1.0f)]
        public float r, g, b, a;
        public UnityEngine.Color ToUnityColour()
        {
            return new UnityEngine.Color(r, g, b, a);
        }
    }
    public static bool operator==(ItemStruct first, ItemStruct second)
    {
        return first.itemName == second.itemName;
    }
    public static bool operator !=(ItemStruct first, ItemStruct second)
    {
        return first.itemName != second.itemName;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}