using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private ClientLineManager lineManager;
    private HashSet<GameObject> activeClients = new HashSet<GameObject>();
    private int correctOrders;
    private int wrongOrders;

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
