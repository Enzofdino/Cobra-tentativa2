using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    public Transform comidaprefab; // Prefab da comida
    public Cobra cobra; // Referência à cobra

    public Transform currentFood; // Comida atual

    void Start()
    {
        SpawnFood(); // Chama para spawnar a comida no início
    }

    void SpawnFood()
    {
        float width = cobra.pegartamanho();
        float height = cobra.pegarcomprimento();

        Vector2 randomPosition;

        do
        {
            float x = Random.Range(-width / 2 + cobra.tamanhocélula / 2, width / 2 - cobra.tamanhocélula / 2);
            float y = Random.Range(-height / 2 + cobra.tamanhocélula / 2, height / 2 - cobra.tamanhocélula / 2);
            randomPosition = new Vector2(x, y);
        }
        while (posiçãoéocupada(randomPosition));

        // Destrói a comida atual se existir
        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
        }

        // Instancia a nova comida
        currentFood = Instantiate(comidaprefab, randomPosition, Quaternion.identity).transform;
        Debug.Log("Food spawned at: " + randomPosition); // Log para verificar a posição
    }

    bool posiçãoéocupada(Vector2 position)
    {
        // Verifica se a cobra está na mesma posição
        if ((Vector2)cobra.transform.position == position)
        {
            return true;
        }

        // Verifica se algum segmento do corpo da cobra está na mesma posição
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
        Vector2 index = currentFood.position / cobra.tamanhocélula;
        if (Mathf.Abs(index.x - cobra.indexcélula.x) < 0.5f && Mathf.Abs(index.y - cobra.indexcélula.y) < 0.5f)
        {
            SpawnFood(); // Chama para spawnar nova comida
            cobra.aumentartamanho(); // Aumenta o tamanho da cobra
        }
    }
}
