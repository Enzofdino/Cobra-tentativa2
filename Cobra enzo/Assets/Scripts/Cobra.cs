using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    public Transform corpoPrefab;
    public List<Transform> body = new List<Transform>();
    private Vector2 direção;
    public float velocidade = 10.0f;
    public float tamanhocélula = 0.3f;
    public Vector2 indexcélula = Vector2.zero;
    private float tempocélula = 0;
    void Start()
    {
        direção = Vector2.up;
    }


    void Update()
    {
        escolhadireção();
        Movimentação();
        checarcolisãodecorpo();
    }
    void escolhadireção()
    {
        Vector2 newdirection = Vector2.zero;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.y == -1) newdirection = Vector2.down;
        else if (input.y == 1) newdirection = Vector2.up;
        else if (input.x == -1) newdirection = Vector2.left;
        else if (input.x == 1) newdirection = Vector2.right;
        if (newdirection + newdirection != Vector2.zero && newdirection != Vector2.zero)
        {
            direção = newdirection;
        }
    }
    void Movimentação()
    {
        if (Time.time > tempocélula)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].position = body[i - 1].position;
            }
            if (body.Count > 0) body[0].position = (Vector2)transform.position;

            transform.position += (Vector3)direção * tamanhocélula;

            tempocélula = Time.time + 1 / velocidade;
            indexcélula = transform.position / tamanhocélula;
        }

    }
    public void aumentartamanho()
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
            position = body[body.Count - 1].position;

        body.Add(Instantiate(corpoPrefab, position, Quaternion.identity).transform);
       
    }

    void checarcolisãodecorpo()
    {
        if (body.Count < 2) return;

        for (int i = 0; i < body.Count; ++i)
        {
            Vector2 index = body[i].position / tamanhocélula;
            if (Mathf.Abs(index.x - indexcélula.x) < 0.00001f && Mathf.Abs(index.y - indexcélula.y) < 0.00001f)
            {
               
                break;
            }
        }
    }
}
