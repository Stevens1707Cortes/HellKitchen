using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private ClientLineManager lineManager;
    [SerializeField] private ClientSpawner clientSpawner;

    private HashSet<GameObject> activeClients = new HashSet<GameObject>();
    public int correctOrders;
    public int wrongOrders;

    void Start()
    {
        GameObject[] clients = GameObject.FindGameObjectsWithTag("Client");
        foreach (var client in clients)
        {
            activeClients.Add(client);
        }
    }

    private void OnDisable()
    {

        correctOrders = 0;
        wrongOrders = 0;

        clientSpawner.activeClients.Clear();
        activeClients.Clear();

        GameObject[] clients = GameObject.FindGameObjectsWithTag("Client");
        foreach (var client in clients)
        {
            lineManager.DequeueClient();
            Destroy(client);
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

    public void SetCorrectOrder() { correctOrders++; }

    public void SetWrongOrder() { wrongOrders++; }

    public int GetWrongOrderCount() { return wrongOrders; }

    public int GetCorrectOrderCount() { return correctOrders;}
}
