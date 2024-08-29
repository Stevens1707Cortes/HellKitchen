using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level State")]

    [SerializeField] GameObject[] dungeons;
    [SerializeField] GameObject kitchen;

    public bool isKitchen;
    public bool isDungeon;
    public bool isDayUpdated;
    public int day = 0;

    [Header("Dungeon Screen")]
    [SerializeField] GameObject foodCanvas;
    [SerializeField] TMP_Text kidneyText, brainText, heartText;


    [Header("Kitchen Screen")]
    [SerializeField] GameObject clientsCanvas;
    [SerializeField] TMP_Text wrongText, correctText, dayText;

    [SerializeField] private ClientManager clientManager;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        isKitchen = true;
        isDungeon = false;

        foreach (var dungeon in dungeons) 
        {
            dungeon.SetActive(false);
        }
    }
    private void Start()
    {
        if (foodCanvas != null) { foodCanvas.SetActive(false); }
        if (clientsCanvas != null) { clientsCanvas.SetActive(false); }

    }

    private void Update()
    {
        
    }

    //Manejo del estado de los niveles
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

    //Manejo de los canvas

    public void ShowFood()
    {
        if (foodCanvas != null)
        {
            foodCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideFood()
    {
        
        foodCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void UpdateKidney(int kidney)
    {
        kidneyText.text = kidney.ToString();
    }

    public void UpdateBrain(int brain)
    {
        brainText.text = brain.ToString();
    }

    public void UpdateHeart(int heart)
    {
        heartText.text = heart.ToString();
    }

    // Clientes

    public void ShowClients()
    {
        if (clientsCanvas != null)
        {
            UpdateWrong(clientManager.wrongOrders);
            UpdateCorrect(clientManager.correctOrders);
            UpdateDay();
            clientsCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideClients()
    {
        clientsCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.isKitchenVictory = false;

    }

    public void UpdateDay()
    {
        day++;
        dayText.text = "Day: " + day.ToString();
        isDayUpdated = true;
    }

    public void UpdateWrong(int wrong)
    {
        wrongText.text = wrong.ToString();
    }

    public void UpdateCorrect(int correct)
    {
        correctText.text = correct.ToString();
    }
}   

