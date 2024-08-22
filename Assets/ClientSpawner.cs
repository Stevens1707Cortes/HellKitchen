using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public GameObject clientPrefab; // Prefab del cliente
    public Transform[] spawnPoints; // Puntos de aparición
    public float spawnInterval = 5f; // Intervalo entre apariciones

    void Start()
    {
        StartCoroutine(SpawnClients());
    }

    IEnumerator SpawnClients()
    {
        while (true)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject client = Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Inicia el comportamiento del cliente
            client.GetComponent<ClientOrder>().Initialize();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
