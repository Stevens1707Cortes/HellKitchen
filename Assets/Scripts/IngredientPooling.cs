using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientPooling : MonoBehaviour
{
    [SerializeField] private GameObject ingredientPrefab;
    public Transform spawnPoint;
    [SerializeField] private int ingredientsNumber;
    [SerializeField] private int maxActivations; // Número máximo de ingredientes en el pool
    [SerializeField] private int maxActiveIngredients = 1; // Máximo de ingredientes activos

    [SerializeField] TMP_Text countText;
    private int countNumber;

    private List<GameObject> ingredients;
    private Quaternion originalRotation;
    [SerializeField]private int activationCounts = 1;

    private void Awake()
    {
        // Inicializar la lista para los ingredientes
        ingredients = new List<GameObject>();

        // Prellenar el pool con los ingredientes
        for (int i = 0; i < ingredientsNumber; i++)
        {
            GameObject ingredient = Instantiate(ingredientPrefab);
            ingredient.SetActive(false);
            ingredient.transform.SetParent(transform); // Organizar los ingredientes en el pool
            ingredients.Add(ingredient);
        }
    }

    private void Start()
    {
        ActivateIngredient();
        countNumber = maxActivations;
        countText.text = countNumber.ToString();
    }

    public void SetMaxActivation(int activations)
    {
        maxActivations = activations;
    }

    // Método para activar un ingrediente
    public GameObject ActivateIngredient()
    {
        if (activationCounts < maxActiveIngredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (!ingredient.activeInHierarchy)
                {
                    ingredient.SetActive(true);
                    ingredient.transform.position = spawnPoint.position; // Posicionar en el punto de spawn
                    originalRotation = ingredient.transform.rotation;
                    activationCounts++;
                    countNumber--;
                    countText.text = countNumber.ToString();
                    return ingredient;
                }
            }
        }
        return null; // No hay ingredientes disponibles para activar
    }

    // Método para desactivar un ingrediente
    public void DeactivateIngredient(GameObject ingredient)
    {
        if (ingredient != null && ingredient.activeInHierarchy)
        {
            ingredient.SetActive(false);
            activationCounts--;
        }
    }

    public void ActivateOneIngredient(GameObject ingredient)
    {
        if (ingredient != null && !ingredient.activeInHierarchy && activationCounts < maxActivations)
        {
            ingredient.transform.rotation = originalRotation;
            ingredient.transform.position = spawnPoint.position;
            ingredient.SetActive(true);
            activationCounts++;
            countNumber--;
            countText.text = countNumber.ToString();
        }
    }
}
