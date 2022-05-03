using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public void Test(Dialogue dialogue)
    {
        PlayerData.instance.dMan.StartDialogue(dialogue);
    }
}
