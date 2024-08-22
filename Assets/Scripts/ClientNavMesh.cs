using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClientNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ClientLineManager lineManager;
    private ClientBehavior clientBehavior;
    private float timer;
    public Transform target;
    public Vector3 originalPosition;
    private Transform endPoint; // Añadido para el punto final

    private void Start()
    {
        lineManager = FindFirstObjectByType<ClientLineManager>();
        clientBehavior = GetComponent<ClientBehavior>();
        agent = GetComponent<NavMeshAgent>();

        originalPosition = transform.position;
        timer = clientBehavior.clientTimer;
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
        StartCoroutine(ArrivalEndPoint());
    }

    public void StartAttendedEndPoint() 
    {
        StartCoroutine(AttendedEndPoint());
    }

    private IEnumerator ArrivalEndPoint()
    {
        // Espera 10 segundos en el primer waitPoint
        yield return new WaitForSeconds(timer);

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

    private IEnumerator AttendedEndPoint()
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
