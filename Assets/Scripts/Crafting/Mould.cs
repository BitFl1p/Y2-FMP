using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mould : MonoBehaviour
{
    public float material;
    public float maxMaterial;
    public Slider matSlider;
    private void Start()
    {
        matSlider.maxValue = maxMaterial;
    }
    private void Update()
    {
        matSlider.value = material;
    }

}
