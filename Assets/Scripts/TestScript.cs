using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public bool move;
    private void Update()
    {
        var trans = transform.position;
        if (move)
        {
            trans.x++;
            move = false;
        }
    }
}
