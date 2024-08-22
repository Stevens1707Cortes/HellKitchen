using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientLineManager : MonoBehaviour
{
    /*Notas: 
    Queue = Cola
    Enqueue = Agregar elemento al final de la cola
    Dequeue = Sacar el primer elemento de la cola

    Es parecido a una lista, pero cuando estamos removiendo elememtos, sale el primero de la cola. 
    Es util para manejar una fila de espera, donde el cliente de al frente tiene que salir, y ser reemplazado por el siguiente
    */

    //Puntos de espera en la fila
    public List<Transform> waitPoints;
    // Cola de los clientes
    private Queue<GameObject> clientQueue = new Queue<GameObject>();

    //Metodos

    // Agregar cliente a la cola
    public void EnqueueClient(GameObject client) 
    {
        clientQueue.Enqueue(client);
        UpdateLinePositions();
    }

    // Quitar cliente de la lista
    public void DequeueClient() 
    {
        if (clientQueue.Count > 0)
        {
            GameObject clientAttended = clientQueue.Dequeue();
            Vector3 originalPosition = clientAttended.GetComponent<ClientNavMesh>().originalPosition;
            clientAttended.GetComponent<ClientNavMesh>().MoveToPosition(originalPosition);
            clientAttended.SetActive(false);
            UpdateLinePositions();
        }
    }

    // Actualizar la posicion al siguiente punto de espera en la cola
    public void UpdateLinePositions()
    {
        int i = 0;
        foreach (var client in clientQueue)
        {
            client.GetComponent<ClientNavMesh>().target = waitPoints[i];
            client.GetComponent<ClientNavMesh>().MoveToPosition(waitPoints[i].position);
            i++;
        }
    }

}
