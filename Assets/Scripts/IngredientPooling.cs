using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPooling : MonoBehaviour
{
    [SerializeField] private GameObject ingredientPrefab; 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxActivations;
    [SerializeField] private int maxActiveIngredients = 1; // Máximo de ingredientes activos

    public int totalIngredientToSpawn;
    private Queue<GameObject> ingredientPool = new Queue<GameObject>();
    private int activationCounts = 0;

    void Start()
    {
        StartCoroutine(SpawnIngredientsRoutine());
    }

    private void SpawnIngredient()
    {
        if (activationCounts < maxActivations)
        {
            GameObject ingredient;
            if (ingredientPool.Count > 0)
            {
                ingredient = ingredientPool.Dequeue();
            }
            else
            {
                ingredient = Instantiate(ingredientPrefab);
            }

            ingredient.transform.position = spawnPoint.position;
            ingredient.SetActive(true);

            // Añadir un componente para manejar la desactivación
            Pickup behavior = ingredient.GetComponent<Pickup>();
            
            behavior.SetPoolingComponent(this); // Referencia a este script

            ingredientPool.Enqueue(ingredient);
            activationCounts++; // Incrementa el contador de activaciones
        }
    }

    public void SetIngredientNumber(int ingredient)
    {
        totalIngredientToSpawn = ingredient;
    }

    public void ReturnIngredientToPool(GameObject ingredient)
    {
        ingredient.transform.position = spawnPoint.position; // Vuelve a la posición de spawn
        ingredient.SetActive(false); // Desactiva el objeto
        ingredientPool.Enqueue(ingredient); // Reintegrar al pool
        activationCounts--; // Decrementa el contador de activaciones
    }

    IEnumerator SpawnIngredientsRoutine()
    {
        while (totalIngredientToSpawn > 0 || ingredientPool.Count < maxActiveIngredients)
        {
            if (activationCounts < maxActivations && totalIngredientToSpawn > 0)
            {
                SpawnIngredient();
                totalIngredientToSpawn--;
            }

            yield return new WaitForSeconds(1f);
        }
    }

}
