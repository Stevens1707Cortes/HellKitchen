using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{
    // Colores para el cliente
    public Color colorVerde = Color.green;
    public Color colorMorado = Color.magenta;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.GetComponent<Pickup>().foodName == "Lungs")
            {
                Destroy(other.gameObject);
                rend.material.color = colorVerde;
            }
            else
            {
                rend.material.color = colorMorado;
            }
        }
        
    }
}
