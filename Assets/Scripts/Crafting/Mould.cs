using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mould : MonoBehaviour
{
    public Transform hammer;
    public Crucible crucible;
    public ItemObject item;
    public float material, lastMaterial;
    public float maxMaterial;
    public Slider matSlider;
    public Button makeSwordButton;
    public GameObject lightSwordPrefab, balancedSwordPrefab, heavySwordPrefab;
    public Transform swordOrigin;
    private void Start()
    {
        matSlider.maxValue = maxMaterial; 
        makeSwordButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (lastMaterial == material) return;
        if(material > maxMaterial / 3)
        {
            makeSwordButton.gameObject.SetActive(true);
            switch (material)
            {
                case var _ when material < (maxMaterial / 3) * 1.5:
                    makeSwordButton.onClick.RemoveAllListeners();
                    makeSwordButton.onClick.AddListener(MakeLightSword);
                    makeSwordButton.GetComponentInChildren<TMP_Text>().text = "Make Light Sword";
                    break;
                case var _ when material < (maxMaterial / 3) * 2.5:
                    makeSwordButton.onClick.RemoveAllListeners();
                    makeSwordButton.onClick.AddListener(MakeBalancedSword);
                    makeSwordButton.GetComponentInChildren<TMP_Text>().text = "Make Balanced Sword";
                    break;
                case var _ when material < maxMaterial:
                    makeSwordButton.onClick.RemoveAllListeners();
                    makeSwordButton.onClick.AddListener(MakeHeavySword);
                    makeSwordButton.GetComponentInChildren<TMP_Text>().text = "Make Heavy Sword";
                    break;
            }
        }
        else
        {
            makeSwordButton.gameObject.SetActive(false);
        }
        matSlider.value = material;
        lastMaterial = material;
    }
    public void MakeLightSword()
    {
        foreach (Transform child in swordOrigin) Destroy(child.gameObject);
        var sword = Instantiate(lightSwordPrefab, swordOrigin);
        var checkLine = sword.GetComponentInChildren<SwordCheck>();
        checkLine.swordMaterial = item;
        hammer.position = new Vector2(hammer.position.x, 2);
        CameraController.instance.ActiveRoom = 2;
        crucible.DumpMaterial();
    }
    public void MakeBalancedSword()
    {
        foreach (Transform child in swordOrigin) Destroy(child.gameObject);
        var sword = Instantiate(balancedSwordPrefab, swordOrigin);
        var checkLine = sword.GetComponentInChildren<SwordCheck>();
        checkLine.swordMaterial = item;
        hammer.position = new Vector2(hammer.position.x, 2);
        CameraController.instance.ActiveRoom = 2; 
        crucible.DumpMaterial();
    }
    public void MakeHeavySword()
    {
        foreach (Transform child in swordOrigin) Destroy(child.gameObject);
        var sword = Instantiate(heavySwordPrefab, swordOrigin);
        var checkLine = sword.GetComponentInChildren<SwordCheck>();
        checkLine.swordMaterial = item;
        hammer.position = new Vector2(hammer.position.x, 2);
        CameraController.instance.ActiveRoom = 2;
        crucible.DumpMaterial();
    }

}
