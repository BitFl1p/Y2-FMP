using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crucible : MonoBehaviour
{
    public ItemObject item; 
    public float material;
    public float maxMaterial;
    public Slider fill;
    public Slider pourer;
    public Transform crucible;
    public CrucibleFollowMouse mouseFollower;
    public Mould mould;
    public GameObject dumpButton;
    public void DumpMaterial()
    {
        material = 0;
        item = null;
    }
    void Start()
    {
        fill.maxValue = maxMaterial;
    }
    void Update()
    {
        if (material > 0) dumpButton.SetActive(true);
        else dumpButton.SetActive(false); 
        material = Mathf.Clamp(material, 0, maxMaterial);
        fill.value = material;
        float angle = mouseFollower.currentAngle;
        float maxAngle = mouseFollower.maxAngle;
        maxAngle -= 360;
        if (mouseFollower.clickedOn && material > 0 && mould.item == item)
        {
            float takeAwayVal = Mathf.Clamp((pourer.value - 0.60f) * 3 * Time.deltaTime, 0, 1);
            material -= takeAwayVal;
            mould.material += takeAwayVal;
            pourer.value = maxAngle - angle + 2 + material/10;
            
        }
        else
        {
            pourer.value = 0;
        }
    }
}
