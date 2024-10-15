using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    public Transform foodPrefab;
    public Cobra cobra;

    private Transform currentFood;

    void Start()
    {
        SpawnFood();
    }

    void SpawnFood()
    {
       
        float width = Cobra.cobra.pegartamanho();
        float height = Cobra.cobra.pegarcomprimento();

        Vector2 randomPosition;

        
        do
        {
            float x = Random.Range(-width / 2 + Cobra.cobra.tamanhoc�lula / 2, width / 2 - Cobra.cobra.tamanhoc�lula / 2);
            float y = Random.Range(-height / 2 + Cobra.cobra.tamanhoc�lula / 2, height / 2 - Cobra.cobra.tamanhoc�lula / 2);
            randomPosition = new Vector2(x, y);
        }
        while (posi��o�ocupada(randomPosition));

       
        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
        }

        
        currentFood = Instantiate(foodPrefab, randomPosition, Quaternion.identity).transform;
    }

   
    bool posi��o�ocupada(Vector2 position)
    {
       
        if ((Vector2)Cobra.cobra.transform.position == position)
        {
            return true;
        }

       
        foreach (Transform segment in Cobra.cobra.body)
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
        
        Vector2 index = currentFood.position / Cobra.cobra.tamanhoc�lula;
        if (Mathf.Abs(index.x - Cobra.cobra.indexc�lula.x) < 0.5f && Mathf.Abs(index.y - Cobra.cobra.indexc�lula.y) < 0.5f)
        {
            
            SpawnFood();
            Cobra.cobra.aumentartamanho(); 
        }
    }
}
