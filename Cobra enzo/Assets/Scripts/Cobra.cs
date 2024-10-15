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
    private Vector2 dire��o;
    public float velocidade = 10.0f;
    public float tamanhoc�lula = 0.3f;
    public Vector2 indexc�lula = Vector2.zero;
    private float tempoc�lula = 0;
    private float alturaparede;
    private float comprimentoparede;
    private bool gameOver = false;

    void Start()
    {
        dire��o = Vector2.up;
    }


    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) gameManager.Restart();
            return;
        }
        escolhadire��o();
        Movimenta��o();
        checarcolis�odecorpo();
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
    void Movimenta��o()
    {
        if (Time.time > tempoc�lula)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].position = body[i - 1].position;
            }
            if (body.Count > 0) body[0].position = (Vector2)transform.position;

            transform.position += (Vector3)dire��o * tamanhoc�lula;

            tempoc�lula = Time.time + 1 / velocidade;
            indexc�lula = transform.position / tamanhoc�lula;
            
        }

    }
    public void aumentartamanho()
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
            position = body[body.Count - 1].position;

        body.Add(Instantiate(corpoPrefab, position, Quaternion.identity).transform);
       
    }

    void checarcolis�odecorpo()
    {
        if (body.Count < 2) return;

        for (int i = 0; i < body.Count; ++i)
        {
            Vector2 index = body[i].position / tamanhoc�lula;
            if (Mathf.Abs(index.x - indexc�lula.x) < 0.00001f && Mathf.Abs(index.y - indexc�lula.y) < 0.00001f)
            {
                GameOver();
                break;
            }
        }
    }
    void criarparedes(float width, float height)
    {
        // Armazenar largura e altura da �rea de jogo
        tamanhoc�lula = width;
        comprimentoparede = height;

        // Calcular os limites da �rea de jogo
        int cellX = Mathf.FloorToInt(width / tamanhoc�lula / 2);
        int cellY = Mathf.FloorToInt(height / tamanhoc�lula / 2);

        // Limpar paredes existentes
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(wall);
        }

        // Criar paredes superior e inferior
        for (int i = -cellX; i <= cellX; i++)
        {
            Vector2 top = new Vector2(i * tamanhoc�lula, cellY * tamanhoc�lula);
            Vector2 bottom = new Vector2(i * tamanhoc�lula, -cellY * tamanhoc�lula);
            Instantiate(paredePrefab, top, Quaternion.identity).tag = "Wall";
            Instantiate(paredePrefab, bottom, Quaternion.identity).tag = "Wall";
        }

        // Criar paredes esquerda e direita
        for (int i = -cellY; i <= cellY; i++)
        {
            Vector2 left = new Vector2(-cellX * tamanhoc�lula, i * tamanhoc�lula);
            Vector2 right = new Vector2(cellX * tamanhoc�lula, i * tamanhoc�lula);
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

        // Resetar posi��o da cobra
        transform.position = Vector3.zero;
    }
}

