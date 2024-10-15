using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    private Vector2 dire��o;
    void Start()
    {
        dire��o = Vector2.up;
    }

   
    void Update()
    {
        escolhadire��o();
    }
    void escolhadire��o()
    {
        Vector2 newdirection = Vector2.zero;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.y == -1) newdirection = Vector2.down;
        else if (input.y == 1) newdirection = Vector2.up;
        else if (input.x == -1) newdirection = Vector2.left;
        else if (input.x == 1) newdirection = Vector2.right;
        if (newdirection + newdirection != Vector2.zero && newdirection != Vector2.zero)
        {
            dire��o = newdirection;
        }
    }
}
