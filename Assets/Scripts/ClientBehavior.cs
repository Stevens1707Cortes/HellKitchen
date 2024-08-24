using System.Collections;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{   
    private ClientManager clientManager;
    private ClientLineManager lineManager;
    private ClientNavMesh navMesh;
    private ClientOrder clientOrder;

    
    // Temporizador Cliente
    [SerializeField] private string foodOrder;
    public float clientTimer;

    //Animaciones
    private Animator animator;
    private bool isAttended = false;
    private bool isWrongOrder = false;

    //Evento para manejar la destruccion de cliente y quitarlo de las listas
    public delegate void ClientDestroyedAction();
    public event ClientDestroyedAction OnDestroyed;


    void Start()
    {   
        //rend = GetComponent<Renderer>();
        animator = GetComponent<Animator>();

        lineManager = FindFirstObjectByType<ClientLineManager>();
        navMesh = GetComponent<ClientNavMesh>();

        clientManager = FindAnyObjectByType<ClientManager>();
        clientManager.RegisterClient(gameObject);

        clientOrder = GetComponent<ClientOrder>();

    }

    private void Update()
    {
        if (isAttended == false) 
        {
            isAttended = clientOrder.hasReceivedOrder;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.GetComponent<Pickup>().foodName == foodOrder)
            {
                Destroy(other.gameObject);
                isAttended = true;
                isWrongOrder = false;
            }
            else
            {
                Destroy(other.gameObject);
                isAttended = false;
                isWrongOrder = true;
            }

            // Cancelar la espera si el cliente recibe el pedido (correcto o incorrecto)
            StopCoroutine(WaitAnimation());
            HandleAttendedRoutines();
           
        }

        if (other.gameObject.CompareTag("Transformable"))
        {   
            Destroy(other.gameObject);

            isWrongOrder = true;

            StopCoroutine(WaitAnimation());
            HandleAttendedRoutines();
        }
        
    }

    public void HandleAttendedRoutines() 
    {
        StartCoroutine(WaitAnimation());
    }

    private void DestroyClient()
    {
        lineManager.DequeueClient();

        // Notificar al ClientManager que el cliente est� siendo destruido
        clientManager.UnregisterClient(gameObject);

        // Notificar a los suscriptores que el cliente est� siendo destruido
        OnDestroyed?.Invoke();

        // Destruir el cliente
        Destroy(gameObject);
    }

    // Corrutinas

    public void StartClientTimer()
    {
        StartCoroutine(ClientTimer());
    }

    IEnumerator ClientTimer()
    {
        yield return new WaitForSeconds(0.2f); 
        DestroyClient();
        
    }

    private IEnumerator WaitAnimation()
    {
        // Esperar el tiempo del clientTimer
        float elapsedTime = 0f;
        while (elapsedTime < clientTimer)
        {
            if (isAttended || isWrongOrder) 
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (!isAttended)
        {
            clientManager.SetWrongOrder();
            animator.SetBool("isSad", true);
        }

        // Verificar si la orden fue correcta o no
        if (isAttended && !isWrongOrder)
        {
            clientManager.SetCorrectOrder();
            animator.SetBool("isHappy", true);
        }
        else if (isWrongOrder)
        {
            clientManager.SetWrongOrder();
            animator.SetBool("isSad", true);
        }


        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        navMesh.StartAttendedEndPoint(); 
        animator.SetBool("isWalking", true); 
    }
}
