using UnityEngine;
using UnityEngine.AI;

public class ClientNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ClientLineManager lineManager;
    private ClientBehavior clientBehavior;
    public Transform target;
    public Vector3 originalPosition;

    private void Start()
    {
        lineManager = FindFirstObjectByType<ClientLineManager>();
        clientBehavior = GetComponent<ClientBehavior>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

        lineManager.EnqueueClient(gameObject);

        MoveToPosition(target.position);

    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            if (target == lineManager.waitPoints[0])
            {
                if (clientBehavior != null)
                {
                    clientBehavior.StartClientTimer();
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

    
}
