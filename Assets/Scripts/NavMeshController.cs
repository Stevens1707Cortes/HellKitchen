using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Animator animator;
    private NavMeshAgent agent;
    private bool playerDetected = false; // Deteccion de enemigo

    // Variables para ajustar la separación
    [SerializeField] private float randomOffsetRange = 20.0f;
    [SerializeField] private float speedVariation = 0.5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ajustar velocidad con una ligera variación
        agent.speed += Random.Range(-speedVariation, speedVariation);
    }

    void Update()
    {
        if (playerDetected) { 
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        // Añadir un desplazamiento aleatorio al destino
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0,
            Random.Range(-randomOffsetRange, randomOffsetRange)
        );

        agent.destination = playerPosition.position + randomOffset;

        if (agent.destination != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void PlayerDetected(bool detected)
    {
        playerDetected = detected;
        if (!detected)
        {
            // Detener movimiento si el jugador no está detectado
            agent.destination = transform.position;
            animator.SetBool("isMoving", false);
        }
    }
}
