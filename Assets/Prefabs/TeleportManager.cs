using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform[] teleportPoints;
    [SerializeField] private Transform kitchenPortal;
    [SerializeField] private Transform loadPortal;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private float teleportDelay = 0.5f;

    private Transform playerTransform;
    public bool isTeleporting = false;
    private int currentPortalIndex = 0;
    private bool isKitchenPortal = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartTeleport()
    {
        StartCoroutine(TeleportPlayer());
    }

    private IEnumerator TeleportPlayer()
    {
        isTeleporting = true;

        // Mostrar pantalla de carga
        //if (loadingScreen != null)
        //{
        //    loadingScreen.SetActive(true);
        //}

        yield return new WaitForSeconds(0.2f);

        TeleportLoad();

        yield return new WaitForSeconds(0.5f);

        levelManager.UnloadKitchen();
        levelManager.UnloadDungeon(currentPortalIndex);

        // Esperar el tiempo de espera antes de teletransportar
        yield return new WaitForSeconds(teleportDelay);

        // Si es el portal de la cocina, hacer el ciclo entre los portales
        if (isKitchenPortal)
        {
            currentPortalIndex = (currentPortalIndex + 1) % teleportPoints.Length;
            levelManager.LoadDungeon(currentPortalIndex);
            playerTransform.position = teleportPoints[currentPortalIndex].position;
        }
        else
        {
            levelManager.LoadKitchen();
            // Si es un portal de mazmorra, teletransportar al portal de la cocina
            playerTransform.position = kitchenPortal.position;
        }

        // Ocultar la pantalla de carga
        //if (loadingScreen != null)
        //{
        //    loadingScreen.SetActive(false);
        //}

        isTeleporting = false;
    }

    public void TeleportLoad()
    {
        playerTransform.position = loadPortal.position;
    }

    public void SetKitchenPortal(bool isKitchen)
    {
        isKitchenPortal = isKitchen;
    }
}
