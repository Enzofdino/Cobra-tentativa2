using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    public Transform comidaprefab; // Prefab da comida
    public Cobra cobra; // Refer�ncia � cobra

    public Transform currentFood; // Comida atual

    void Start()
    {
        SpawnFood(); // Chama para spawnar a comida no in�cio
    }

    void SpawnFood()
    {
        float width = cobra.pegartamanho();
        float height = cobra.pegarcomprimento();

        Vector2 randomPosition;

        do
        {
            float x = Random.Range(-width / 2 + cobra.tamanhoc�lula / 2, width / 2 - cobra.tamanhoc�lula / 2);
            float y = Random.Range(-height / 2 + cobra.tamanhoc�lula / 2, height / 2 - cobra.tamanhoc�lula / 2);
            randomPosition = new Vector2(x, y);
        }
        while (posi��o�ocupada(randomPosition));

        // Destr�i a comida atual se existir
        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
        }

        // Instancia a nova comida
        currentFood = Instantiate(comidaprefab, randomPosition, Quaternion.identity).transform;
        Debug.Log("Food spawned at: " + randomPosition); // Log para verificar a posi��o
    }

    bool posi��o�ocupada(Vector2 position)
    {
        // Verifica se a cobra est� na mesma posi��o
        if ((Vector2)cobra.transform.position == position)
        {
            return true;
        }

        // Verifica se algum segmento do corpo da cobra est� na mesma posi��o
        foreach (Transform segment in cobra.body)
        {
            if ((Vector2)segment.position == position)
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        Vector2 index = currentFood.position / cobra.tamanhoc�lula;
        if (Mathf.Abs(index.x - cobra.indexc�lula.x) < 0.5f && Mathf.Abs(index.y - cobra.indexc�lula.y) < 0.5f)
        {
            SpawnFood(); // Chama para spawnar nova comida
            cobra.aumentartamanho(); // Aumenta o tamanho da cobra
        }
    }
}
