using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private HashSet<GameObject> activeClients = new HashSet<GameObject>();
    void Start()
    {
        GameObject[] clients = GameObject.FindGameObjectsWithTag("Client");
        foreach (var client in clients)
        {
            activeClients.Add(client);
        }
    }

    public void RegisterClient(GameObject client)
    {
        activeClients.Add(client);
    }

    public void UnregisterClient(GameObject client)
    {
        activeClients.Remove(client);
    }

    public int GetActiveClientCount()
    {
        return activeClients.Count;
    }
}
