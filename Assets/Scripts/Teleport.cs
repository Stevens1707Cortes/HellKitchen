using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] TeleportManager manager;
    [SerializeField] private bool isKitchen;

    [SerializeField] private GameObject canvasDoor;

    private void Start()
    {
        //Canvas

        if (canvasDoor != null)
        {
            canvasDoor.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasDoor.SetActive(true);
            manager.SetKitchenPortal(isKitchen);

            if (Input.GetKeyDown(KeyCode.E) && !manager.isTeleporting)
            {
                Debug.Log("Pasando de nivel");
                manager.StartTeleport();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canvasDoor != null)
            {
                canvasDoor.SetActive(false);
            }

        }
    }

}
