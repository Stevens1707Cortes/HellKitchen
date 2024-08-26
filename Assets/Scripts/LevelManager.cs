using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] dungeons;
    [SerializeField] GameObject kitchen;

    public bool isKitchen;
    public bool isDungeon;

    private void Awake()
    {
        isKitchen = true;
        isDungeon = false;
        dungeons[0].SetActive(false);
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!isKitchen)
        {
            LoadDungeon(0);
        }
        else
        {
            UnloadDungeon(0);
        }
    }

    public void ResetFood()
    {
        GameObject[] activeFood = GameObject.FindGameObjectsWithTag("Food");
        foreach (var food in activeFood)
        {
            Destroy(food);
        }

        GameObject[] activeIngredient = GameObject.FindGameObjectsWithTag("Transformable");
        foreach (var ingredient in activeIngredient)
        {
            Destroy(ingredient);
        }
    }

    public void LoadDungeon(int dungeon)
    {
        dungeons[dungeon].SetActive(true);
        isDungeon = true;
    }

    public void UnloadDungeon(int dungeon)
    {
        dungeons[dungeon].SetActive(false);
        isDungeon = false;
    }

    public void LoadKitchen()
    {
        kitchen.SetActive(true);
        isKitchen = true;
    }

    public void UnloadKitchen()
    {
        kitchen.SetActive(false);
        isKitchen = false;
    }
}   

