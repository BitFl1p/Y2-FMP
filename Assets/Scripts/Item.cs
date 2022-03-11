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
    public string itemName;
    public ItemType itemType;
    public Sprite sprite;
    public int basePrice;
    public int[] statBoosts;
}
public class Item : MonoBehaviour
{
    public ItemObject item;
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}