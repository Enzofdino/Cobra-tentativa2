using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cobra : MonoBehaviour // Classe responsavel por controlar o comportamento da cobra no jogo.
{
    public Transform corpoprefab;
    public Transform paredePrefab;
    public GameManager gameManager;
    private Vector2 direção;
    private float tempocélula = 0;
    public List<Transform> body = new List<Transform>();
    public float velocidade = 10.0f;
    public float tamanhocélula = 0.3f;
    public Vector2 celulaindex = Vector2.zero;
    private float comprimento;
    private float tamanho;
    private bool gameOver = false;

    void Start() // Metodo chamado no inicio do jogo.
    {
        direção = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) gameManager.Restart(); // Reinicia o jogo se apertar a tecla R apos o game over.
            return;
        }

        escolherdirecao();
        Movimentar();
        checarcolisaocorpo();
    }
    void escolherdirecao() // Metodo para alterar a direcao da cobra com base na entrada do jogador.
    {
        Vector2 newdirection = Vector2.zero;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // Altera a direcao conforme a tecla pressionada.
        if (input.y == -1) newdirection = Vector2.down;
        else if (input.y == 1) newdirection = Vector2.up;
        else if (input.x == -1) newdirection = Vector2.left;
        else if (input.x == 1) newdirection = Vector2.right;
        if (newdirection + newdirection != Vector2.zero && newdirection != Vector2.zero)
        {
            direção = newdirection;
        }
    }
    void Movimentar() // Metodo que move a cobra na direcao definida.
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
            celulaindex = transform.position / tamanhocélula;

            CheckWallWrapAround();
        }
    }
    void CheckWallWrapAround() // Metodo que verifica se a cobra atravessou a parede e ajusta a posicao.
    {
        if (transform.position.x > comprimento / 2)
            transform.position = new Vector3(-comprimento / 2 + 0.01f, transform.position.y, transform.position.z);
        else if (transform.position.x < -comprimento / 2)
            transform.position = new Vector3(comprimento / 2 - 0.01f, transform.position.y, transform.position.z);

        if (transform.position.y > tamanho / 2)
            transform.position = new Vector3(transform.position.x, -tamanho / 2 + 0.01f, transform.position.z);
        else if (transform.position.y < -tamanho / 2)
            transform.position = new Vector3(transform.position.x, tamanho / 2 - 0.01f, transform.position.z);
    }
    public void aumentartamanho()  // Metodo responsavel por aumentar o corpo da cobra ao comer alimento.
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
            position = body[body.Count - 1].position;

        body.Add(Instantiate(corpoprefab, position, Quaternion.identity).transform);
        gameManager.UpdateScore(1);
    }
    void checarcolisaocorpo() // Metodo que verifica se a cobra colidiu com o proprio corpo.
    {
        if (body.Count < 3) return;
        for (int i = 0; i < body.Count; i++)
        {
            Vector2 index = body[i].position / tamanhocélula;
            if (Mathf.Abs(index.x - celulaindex.x) < 0.00001f && Mathf.Abs(index.y - celulaindex.y) < 0.00001f)
            {
                GameOver();
                break;
            }
        }
    }
    void GameOver() // Metodo que termina o jogo ao ocorrer uma colisao.
    {
        gameOver = true;
        gameManager.GameOver();

    }
    public void reiniciar() // Metodo responsavel por reiniciar o jogo e resetar o estado da cobra.
    {
        gameOver = false;

        // Limpar corpo da cobra
        for (int i = 0; i < body.Count; ++i)
        {
            Destroy(body[i].gameObject);
        }
        body.Clear();

        // Resetar posicao da cobra
        transform.position = Vector3.zero;
    }
    public float pegarcomprimento() // Metodo que retorna a largura da area de jogo.
    {
        return comprimento;
    }
    public float pegartamanho()  // Metodo que retorna a altura da area de jogo.
    {
        return tamanho;
    }
    public void colocarvelocidade(float newSpeed) // Metodo para ajustar a velocidade da cobra.
    {
        velocidade = newSpeed;
    }
    public void SetGameArea(float width, float height) // Metodo para ajustar a area de jogo.
    {
        // Ajuste a lógica para criar paredes com base nos novos valores
        criarparedes(width, height);
    }
    void criarparedes(float width, float height)  // Metodo que cria as paredes da area de jogo.
    {
        // Armazenar largura e altura da área de jogo
        tamanho = width;
        comprimento = height;

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
            Instantiate(paredePrefab, top, Quaternion.identity).tag = "Parede";
            Instantiate(paredePrefab, bottom, Quaternion.identity).tag = "Parede";
        }

        // Criar paredes esquerda e direita
        for (int i = -cellY; i <= cellY; i++)
        {
            Vector2 left = new Vector2(-cellX * tamanhocélula, i * tamanhocélula);
            Vector2 right = new Vector2(cellX * tamanhocélula, i * tamanhocélula);
            Instantiate(paredePrefab, left, Quaternion.identity).tag = "Parede";
            Instantiate(paredePrefab, right, Quaternion.identity).tag = "Parede";
        }
    }
}
