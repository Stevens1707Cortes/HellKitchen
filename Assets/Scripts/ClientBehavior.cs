using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{   
    private ClientManager clientManager;

    // Colores para el cliente
    public Color colorVerde = Color.green;
    public Color colorMorado = Color.magenta;
    private Renderer rend;

    void Start()
    {   
        rend = GetComponent<Renderer>();
        clientManager = FindAnyObjectByType<ClientManager>();
        clientManager.RegisterClient(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.GetComponent<Pickup>().foodName == "Lungs")
            {
                Destroy(other.gameObject);
                rend.material.color = colorVerde;
                clientManager.UnregisterClient(gameObject);
            }
            else
            {
                rend.material.color = colorMorado;
                clientManager.UnregisterClient(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Transformable"))
        {   
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
        
    }
}
