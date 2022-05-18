using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public TMP_Text damageText, speedText;
    public ItemObject item;
    public GameObject origin;
    public GameObject panel;
    public Vector2 offset = Vector2.zero;
    #region Singleton Shit
    
    public static ShowStats instance;
    void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }

    }
    #endregion
    void Update()
    {
        if (origin == null) item = null;
        if (item)
        {
            
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            panel.SetActive(true);
            if(item.item.itemType == ItemType.Hilt)
            {
                damageText.text = $"X {item.item.damage}"; 
                speedText.text = $"X {item.item.speed}";
            }
            else
            {
                damageText.text = $"+ {item.item.damage}"; 
                speedText.text = $"X {item.item.speed}";
            }
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
