using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientPooling : MonoBehaviour
{
    [SerializeField] protected GameObject ingredientPrefab;
    public Transform spawnPoint;
    [SerializeField] protected int ingredientsNumber;
    [SerializeField] protected int maxActivations; // Número máximo de ingredientes en el pool
    [SerializeField] protected int maxActiveIngredients = 1; // Máximo de ingredientes activos

    public TMP_Text countText;
    public int countNumber;

    protected List<GameObject> ingredients;
    protected Quaternion originalRotation;
    [SerializeField] protected int activationCounts = 0;

    protected virtual void Awake()
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

    protected virtual void Start()
    {
        ActivateIngredient();
        countNumber = maxActivations;
        countText.text = countNumber.ToString();
    }

    public virtual void Initialize()
    {
        ActivateIngredient();
        countNumber = maxActivations;
        countText.text = countNumber.ToString();
    }

    public virtual void SetMaxActivation(int activations)
    {
        maxActivations = activations;
    }

    // Método para activar un ingrediente
    public virtual GameObject ActivateIngredient()
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
                    //countNumber--;
                    //countText.text = countNumber.ToString();
                    return ingredient;
                }
            }
        }
        return null; // No hay ingredientes disponibles para activar
    }

    // Método para desactivar un ingrediente
    public virtual void DeactivateIngredient(GameObject ingredient)
    {
        if (ingredient != null && ingredient.activeInHierarchy)
        {
            ingredient.SetActive(false);
            activationCounts--;
        }
    }

    public virtual void ActivateOneIngredient(GameObject ingredient)
    {
        if (ingredient != null && !ingredient.activeInHierarchy && activationCounts < maxActivations)
        {
            ingredient.transform.rotation = originalRotation;
            ingredient.transform.position = spawnPoint.position;
            ingredient.SetActive(true);
            activationCounts++;
            //countNumber--;
            //countText.text = countNumber.ToString();
        }
    }
}
