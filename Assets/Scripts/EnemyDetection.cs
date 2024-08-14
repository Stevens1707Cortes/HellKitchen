using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f; // Radio de la esfera de detección
    public LayerMask playerMask; // Máscara para asegurarse de que solo se detecta al jugador
    public float checkInterval = 0.5f; // Intervalo entre comprobaciones

    private float timeSinceLastCheck;

    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= checkInterval)
        {
            DetectPlayer();
            timeSinceLastCheck = 0f;
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);

        foreach (Collider collider in hitColliders)
        {
            if (collider.transform == player)
            {
                Debug.Log("Jugador detectado");
                break; // Salir del bucle si se encuentra al jugador
            }
        }
    }
}
