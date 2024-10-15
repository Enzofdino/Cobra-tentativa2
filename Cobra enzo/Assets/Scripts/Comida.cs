using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Classe responsável por gerar e gerenciar o alimento no jogo.
public class Comida : MonoBehaviour
{
    public Transform comidaPrefab; // Prefab do alimento a ser instanciado.
    public Cobra cobra; // Referência à cobra no jogo.

    private Transform currentFood;
    // Método chamado no início do jogo. Gera o primeiro alimento.
    void Start()
    {
        SpawnFood();
    }

    // Método chamado a cada frame. Verifica se a cobra comeu o alimento.
    void Update()
    {
        // Verifica se a cobra comeu o alimento
        Vector2 index = currentFood.position / cobra.tamanhocélula;
        if (Mathf.Abs(index.x - cobra.celulaindex.x) < 0.5f && Mathf.Abs(index.y - cobra.celulaindex.y) < 0.5f)
        {
            // Se a cobra comeu, gera um novo alimento
            SpawnFood();
            cobra.aumentartamanho(); // Aumenta o corpo da cobra
        }
    }
    // Método que verifica se a posição está ocupada pela cobra.
    bool posicaofoiocupada(Vector2 position)
    {
        // Verifica se a posição coincide com a posição da cabeça da cobra
        if ((Vector2)cobra.transform.position == position)
        {
            return true; // A posição está ocupada pela cabeça
        }

        // Verifica se a posição coincide com algum segmento do corpo da cobra
        foreach (Transform segment in cobra.body)
        {
            if ((Vector2)segment.position == position)
            {
                return true; // A posição está ocupada
            }
        }
        return false; // A posição está livre
    }
    // Método responsável por gerar o alimento em uma posição aleatória.
    void SpawnFood()
    {
        // Obtém a largura e altura da área de jogo da cobra
        float width = cobra.pegartamanho();
        float height = cobra.pegarcomprimento();

        Vector2 randomPosition;

        // Continua gerando uma nova posição até encontrar uma que não esteja ocupada pela cobra
        do
        {
            float x = Random.Range(-width / 2 + cobra.tamanhocélula / 2, width / 2 - cobra.tamanhocélula / 2);
            float y = Random.Range(-height / 2 + cobra.tamanhocélula / 2, height / 2 - cobra.tamanhocélula / 2);
            randomPosition = new Vector2(x, y);
        }
        while (posicaofoiocupada(randomPosition));

        // Remove o alimento anterior, se existir
        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
        }

        // Instancia um novo alimento
        currentFood = Instantiate(comidaPrefab, randomPosition, Quaternion.identity).transform;
    }
}
