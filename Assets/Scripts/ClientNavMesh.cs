using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClientNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ClientLineManager lineManager;
    private ClientBehavior clientBehavior;
    private Animator animator;

    public Transform target;
    public Vector3 originalPosition;
    private Transform endPoint; // Añadido para el punto final

    private void Start()
    {
        lineManager = FindFirstObjectByType<ClientLineManager>();
        clientBehavior = GetComponent<ClientBehavior>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        originalPosition = transform.position;
        endPoint = lineManager.endPoint;

        lineManager.EnqueueClient(gameObject);

        MoveToPosition(target.position);

    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                if (target == lineManager.waitPoints[0])
                {
                    // Metodo que maneja que hacer
                    clientBehavior.HandleAttendedRoutines();
                }
            }
        }

        // Activar la animación de caminar mientras el cliente está en movimiento
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);

    }

    // Método para mover al cliente hacia su lugar o a una posicion
    public void MoveToPosition(Vector3 position)
    {
        if (agent != null)
        {
            agent.SetDestination(position);
        }
    }

    public void StartArrivalEndPoint()
    {
        StartCoroutine(MoveToEndPoint());
        animator.SetBool("isWalking", true);
    }

    public void StartAttendedEndPoint() 
    {
        StartCoroutine(MoveToEndPoint());
        animator.SetBool("isWalking", true);
    }

    private IEnumerator MoveToEndPoint()
    {
        // Mueve el cliente al endPoint
        MoveToPosition(endPoint.position);

        // Espera hasta que el cliente llegue al endPoint
        while (agent.remainingDistance > agent.stoppingDistance || agent.pathPending)
        {
            yield return null;
        }

        // Inicia la corrutina para destruir el cliente
        if (clientBehavior != null)
        {
            clientBehavior.StartClientTimer();
        }
    }

}
