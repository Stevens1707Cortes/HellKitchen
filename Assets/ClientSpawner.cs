using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab; // Prefab del cliente
    [SerializeField] private Transform[] spawnPoints; // Puntos de aparición
    [SerializeField] private int maxActiveClients = 5; // Máximo de clientes activos
    [SerializeField] private float spawnInterval = 3f; // Intervalo entre apariciones

    public int totalClientsToSpawn;
    private List<GameObject> activeClients = new List<GameObject>(); 

    void Start()
    {
        StartCoroutine(SpawnClientsRoutine());
    }

    public void SetClientsNumber(int clients)
    {
        totalClientsToSpawn = clients;
    }

    public void ClientDestroyed(GameObject client)
    {
        activeClients.Remove(client); // Eliminar cliente de la lista de activos
    }

    private void SpawnClient()
    {
        if (activeClients.Count < maxActiveClients && totalClientsToSpawn > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject newClient = Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);
            activeClients.Add(newClient);
            totalClientsToSpawn--;

            // Suscribir al evento de destrucción (si existe)
            ClientBehavior clientScript = newClient.GetComponent<ClientBehavior>();
            if (clientScript != null)
            {
                clientScript.OnDestroyed += () => ClientDestroyed(newClient);
            }
        }
    }

    IEnumerator SpawnClientsRoutine()
    {
        while (totalClientsToSpawn > 0 || activeClients.Count > 0)
        {
            SpawnClient();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
