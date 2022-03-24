using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class HumanOutfitChanger : MonoBehaviour
{
    [System.Serializable]
    public struct Suit
    {
        public List<GameObject> parts;
    }
    public List<Suit> suits;
    public List<int> suitParts = new List<int> { 0, 0, 0, 0 };
    private void Update()
    {
        foreach(Suit suit in suits) foreach(GameObject part in suit.parts)
        {
            part.SetActive(false);
        }
        
        suits[suitParts[0]].parts[0].SetActive(true);
        suits[suitParts[1]].parts[1].SetActive(true);
        suits[suitParts[2]].parts[2].SetActive(true);
        suits[suitParts[3]].parts[3].SetActive(true);
    }
}
