using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientOrder : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Canvas dialogCanvas; // Referencia al Canvas que contiene el texto
    public float activationDistance = 5f; // Distancia mínima para activar el texto
    private bool hasReceivedOrder = false;
    public void Initialize()
    {
        dialogCanvas.gameObject.SetActive(false); // Oculta el cuadro de texto al inicio
        StartCoroutine(ClientTimer());
    }
    void Start()
    {
        dialogCanvas.gameObject.SetActive(false); // Asegura que el cuadro de texto esté desactivado al inicio
        player = GameObject.Find("PlayerFPS");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < activationDistance)
        {
            dialogCanvas.gameObject.SetActive(true); // Muestra el cuadro de texto
            dialogCanvas.transform.Rotate(new Vector3(0, 5f, 0));
            
        }
        else
        {
            dialogCanvas.gameObject.SetActive(false); // Oculta el cuadro de texto cuando el jugador se aleja
        }
    }
    IEnumerator ClientTimer()
    {
        yield return new WaitForSeconds(5f); // Espera 1 minuto
        if (!hasReceivedOrder)
        {
            Destroy(gameObject); // Destruye el cliente si no ha recibido la orden
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "food")
        {
            hasReceivedOrder = true;
            dialogCanvas.gameObject.SetActive(false); // Oculta el cuadro de texto
            Destroy(gameObject); // Destruye el cliente tras recibir la orden
        }
    }
}