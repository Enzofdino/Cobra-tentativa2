using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    #region singleton
<<<<<<< HEAD
    public static Cobra cobra;
=======
    public Cobra cobra;
>>>>>>> 76d0feb368aff55e530ffb4a9da007192eae708b
    public GameManager gameManager;
    private void Awake()
    {
        cobra = this;
    }
    #endregion   
    public Transform corpoPrefab;
    public Transform paredePrefab;
    public List<Transform> body = new List<Transform>();
    private Vector2 direção;
    public float velocidade = 10.0f;
    public float tamanhocélula = 0.3f;
    public Vector2 indexcélula = Vector2.zero;
    private float tempocélula = 0;
    private float alturaparede;
    private float comprimentoparede;
    private bool gameOver = false;

    void Start()
    {
        direção = Vector2.up;
    }


    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) gameManager.Restart();
            return;
        }
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
                GameOver();
                break;
            }
        }
    }
    void criarparedes(float width, float height)
    {
        // Armazenar largura e altura da área de jogo
        tamanhocélula = width;
        comprimentoparede = height;

        // Calcular os limites da área de jogo
        int cellX = Mathf.FloorToInt(width / tamanhocélula / 2);
        int cellY = Mathf.FloorToInt(height / tamanhocélula / 2);

        // Limpar paredes existentes
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(wall);
        }

        // Criar paredes superior e inferior
        for (int i = -cellX; i <= cellX; i++)
        {
            Vector2 top = new Vector2(i * tamanhocélula, cellY * tamanhocélula);
            Vector2 bottom = new Vector2(i * tamanhocélula, -cellY * tamanhocélula);
            Instantiate(paredePrefab, top, Quaternion.identity).tag = "Wall";
            Instantiate(paredePrefab, bottom, Quaternion.identity).tag = "Wall";
        }

        // Criar paredes esquerda e direita
        for (int i = -cellY; i <= cellY; i++)
        {
            Vector2 left = new Vector2(-cellX * tamanhocélula, i * tamanhocélula);
            Vector2 right = new Vector2(cellX * tamanhocélula, i * tamanhocélula);
            Instantiate(paredePrefab, left, Quaternion.identity).tag = "Wall";
            Instantiate(paredePrefab, right, Quaternion.identity).tag = "Wall";
        }
    }
    public void colocartamanhodaarea(float width, float height)
    {
        
        criarparedes(width, height);
    }

    public float pegartamanho()
    {
        return alturaparede;
    }

    public float pegarcomprimento()
    {
        return comprimentoparede;
    }

    public void Colocarvelocidade(float newvelocidade)
    {
        velocidade = newvelocidade;
    }
   public void GameOver()
    {
        gameOver = true;
       
    }

    public  void Restart()
    {
        gameOver = false;

        // Limpar corpo da cobra
        for (int i = 0; i < body.Count; ++i)
        {
            Destroy(body[i].gameObject);
        }
        body.Clear();

        // Resetar posição da cobra
        transform.position = Vector3.zero;
    }
}

