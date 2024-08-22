using System.Collections;
using UnityEngine;

public class ClientOrder : MonoBehaviour
{
    private GameObject player;
    private float activationDistance = 5f; // Distancia mínima para activar el texto

    [SerializeField] private Canvas dialogCanvas; 

    public bool hasReceivedOrder = false;
    
    void Start()
    {
        dialogCanvas.gameObject.SetActive(false); 
        player = GameObject.Find("PlayerFPS");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < activationDistance)
        {
            dialogCanvas.gameObject.SetActive(true); // Muestra el cuadro de texto
            dialogCanvas.transform.Rotate(new Vector3(0, 2f, 0));
            
        }
        else
        {
            dialogCanvas.gameObject.SetActive(false); // Oculta el cuadro de texto cuando el jugador se aleja
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Food")
        {
            hasReceivedOrder = true;
            dialogCanvas.gameObject.SetActive(false); // Oculta el cuadro de texto
        }
    }
}