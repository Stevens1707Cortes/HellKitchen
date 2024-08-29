using System.Collections;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform[] teleportPoints;
    [SerializeField] private Transform kitchenPortal;
    [SerializeField] private Transform loadPortal;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject HudPlayer;
    private PlayerController playerController;
    
    [SerializeField] private float teleportDelay = 0.5f;

    private Transform playerTransform;
    public bool isTeleporting = false;
    private int currentPortalIndex = 0;
    private bool isKitchenPortal = false;

    private void Start()
    {
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    public void StartTeleport()
    {
        StartCoroutine(TeleportPlayer());
    }

    private IEnumerator TeleportPlayer()
    {
        isTeleporting = true;

        // Ocultar HUD y arma actual
        HudPlayer.SetActive(false);
        playerController.HideCurrentWeapon();

        // Mostrar pantalla de carga
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

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

        //Ocultar la pantalla de carga
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        // Mostrar HUD y arma actual
        HudPlayer.SetActive(true);
        playerController.ShowCurrentWeapon();

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

    //Metodo para usar en el boton
    public void TeleportToKitchen()
    {
        StartCoroutine(TeleportToKitchenCoroutine());
    }

    private IEnumerator TeleportToKitchenCoroutine()
    {
        isTeleporting = true;

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        yield return new WaitForSeconds(teleportDelay);

        levelManager.UnloadDungeon(currentPortalIndex);
        levelManager.LoadKitchen();

        playerTransform.position = kitchenPortal.position;

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        isTeleporting = false;

        levelManager.HideFood();
    }
}
