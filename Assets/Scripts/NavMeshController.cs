using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private string actionZone;

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool playerDetected = false; // Deteccion de enemigo
    private string detectedZoneTag = "";


    // Variables para ajustar la separación
    [SerializeField] private float randomOffsetRange = 21.0f;
    [SerializeField] private float speedVariation = 0.5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerPosition = GameObject.Find("Player").transform;

        originalPosition = transform.position;

        // Ajustar velocidad con una ligera variación
        agent.speed += Random.Range(-speedVariation, speedVariation);
    }

    void Update()
    {
        
    }

    public void MoveEnemy()
    {
        // Añadir un desplazamiento aleatorio al destino
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0,
            Random.Range(-randomOffsetRange, randomOffsetRange)
        );

        agent.isStopped = false; // Asegurarse de que el agente no esté detenido
        agent.destination = playerPosition.position + randomOffset;

    }

    public void ReturnOriginalPosition()
    {
        // Compara la distancia entre la posición actual y la original
        if (Vector3.Distance(transform.position, originalPosition) < 0.2f)
        {
            // Si el enemigo está suficientemente cerca de la posición original, detener el agente
            agent.isStopped = true;
        }
        else
        {
            // Establecer el destino como la posición original y reanudar el agente si estaba detenido
            agent.isStopped = false;
            agent.destination = originalPosition;
        }
    }

    public void PlayerDetected(bool detected, string zoneTag = "")
    {
        playerDetected = detected;
        detectedZoneTag = zoneTag;

        //if (!detected)
        //{
        //    ReturnOriginalPosition();
        //}
    }

    public bool IsPlayerDetected()
    {
        return playerDetected && detectedZoneTag == actionZone;
    }

    public bool HasReturnedToOriginalPosition()
    {
        return Vector3.Distance(transform.position, originalPosition) < 0.2f;
    }

    public bool IsMoving()
    {
        return agent.velocity.magnitude > 0.1f;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

}
