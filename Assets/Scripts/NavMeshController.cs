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


    // Variables para ajustar la separaci�n
    [SerializeField] private float randomOffsetRange = 21.0f;
    [SerializeField] private float speedVariation = 0.5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        originalPosition = transform.position;

        // Ajustar velocidad con una ligera variaci�n
        agent.speed += Random.Range(-speedVariation, speedVariation);
    }

    void Update()
    {
        if (playerDetected)
        {
            if (detectedZoneTag == actionZone)
            {
                MoveEnemy();
            }
            else
            {
                ReturnOriginalPosition();
            }
        }

        // Activar o desactivar la animaci�n en funci�n de la velocidad del agente
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveEnemy()
    {
        // A�adir un desplazamiento aleatorio al destino
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0,
            Random.Range(-randomOffsetRange, randomOffsetRange)
        );

        agent.isStopped = false; // Asegurarse de que el agente no est� detenido
        agent.destination = playerPosition.position + randomOffset;

    }

    void ReturnOriginalPosition()
    {
        // Compara la distancia entre la posici�n actual y la original
        if (Vector3.Distance(transform.position, originalPosition) < 0.2f)
        {
            // Si el enemigo est� suficientemente cerca de la posici�n original, detener el agente
            agent.isStopped = true;
        }
        else
        {
            // Establecer el destino como la posici�n original y reanudar el agente si estaba detenido
            agent.isStopped = false;
            agent.destination = originalPosition;
        }
    }

    public void PlayerDetected(bool detected, string zoneTag = "")
    {
        playerDetected = detected;
        detectedZoneTag = zoneTag;

        if (!detected)
        {
            ReturnOriginalPosition();
        }
    }
}
