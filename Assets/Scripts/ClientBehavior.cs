using System.Collections;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{   
    private ClientManager clientManager;
    private ClientLineManager lineManager;
    private ClientNavMesh navMesh;

    // Colores para el cliente
    //public Color colorVerde = Color.green;
    //public Color colorMorado = Color.magenta;
    //private Renderer rend;

    // Temporizador Cliente
    [SerializeField] private string foodOrder;
    public float clientTimer;

    //Animaciones
    private Animator animator;
    private bool isAttended = false;
    
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

        foodOrder = "Lungs";
    }

    private void Update()
    {
        if (isAttended == false) 
        {
            isAttended = gameObject.GetComponent<ClientOrder>().hasReceivedOrder;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.GetComponent<Pickup>().foodName == foodOrder)
            {
                Destroy(other.gameObject);
                //rend.material.color = colorVerde;
                isAttended = true;
            }
            else
            {
                //rend.material.color = colorMorado;
                isAttended = true;
            }

            // Cancelar la espera si el cliente recibe el pedido
            if (isAttended)
            {
                StopCoroutine(WaitAnimation());
                HandleAttendedRoutines(); 
            }
        }

        if (other.gameObject.CompareTag("Transformable"))
        {   
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
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
        // Esperar en el punto de espera 0 por el tiempo especificado en clientTimer
        float elapsedTime = 0f;
        while (elapsedTime < clientTimer)
        {
            if (isAttended)
            {
                break; 
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Verificar si el cliente fue atendido y ejecutar la animaci�n correspondiente
        if (isAttended)
        {
            animator.SetBool("isHappy", true);
        }
        else
        {
            animator.SetBool("isSad", true);
        }


        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Caminar punto final
        navMesh.StartAttendedEndPoint(); 
        animator.SetBool("isWalking", true); 
    }
}
