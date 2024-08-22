using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{   
    private ClientManager clientManager;
    private ClientLineManager lineManager;
    private ClientNavMesh navMesh;

    // Colores para el cliente
    public Color colorVerde = Color.green;
    public Color colorMorado = Color.magenta;
    private Renderer rend;

    public float clientTimer;
    [SerializeField] private string foodOrder;
    private bool isAttended = false;

    public delegate void ClientDestroyedAction();
    public event ClientDestroyedAction OnDestroyed;


    void Start()
    {   
        rend = GetComponent<Renderer>();

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
                rend.material.color = colorVerde;
                isAttended = true;
                //clientManager.UnregisterClient(gameObject);
            }
            else
            {
                rend.material.color = colorMorado;
                isAttended = true;
                //clientManager.UnregisterClient(gameObject);
            }

            //Destruir el cliente luego de recibir la orden

        }

        if (other.gameObject.CompareTag("Transformable"))
        {   
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
        
    }

    public void HandleAttendedRoutines() 
    {
        if (isAttended == false) 
        { 
            navMesh.StartArrivalEndPoint();
        }
        else if(isAttended == true)
        {
            navMesh.StartAttendedEndPoint();
        }
    }

    public void StartClientTimer()
    {
        StartCoroutine(ClientTimer());
    }

    IEnumerator ClientTimer()
    {
        yield return new WaitForSeconds(0.2f); 
        DestroyClient();
        
    }

    private void DestroyClient()
    {
        lineManager.DequeueClient();

        // Notificar al ClientManager que el cliente está siendo destruido
        clientManager.UnregisterClient(gameObject);

        // Notificar a los suscriptores que el cliente está siendo destruido
        OnDestroyed?.Invoke();

        // Destruir el cliente
        Destroy(gameObject);
    }
}
