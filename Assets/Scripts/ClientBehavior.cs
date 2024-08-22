using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{   
    private ClientManager clientManager;
    private ClientLineManager lineManager;

    // Colores para el cliente
    public Color colorVerde = Color.green;
    public Color colorMorado = Color.magenta;
    private Renderer rend;

    [SerializeField] private float clientTimer;
    [SerializeField] private string foodOrder;
    private bool isAttended = false;

    public delegate void ClientDestroyedAction();
    public event ClientDestroyedAction OnDestroyed;


    void Start()
    {   
        rend = GetComponent<Renderer>();

        lineManager = GameObject.FindFirstObjectByType<ClientLineManager>();

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
                //clientManager.UnregisterClient(gameObject);
            }
            else
            {
                rend.material.color = colorMorado;
                //clientManager.UnregisterClient(gameObject);
            }

            //Destruir el cliente luego de recibir la orden
            Invoke("DestroyClient", 2f);
        }

        if (other.gameObject.CompareTag("Transformable"))
        {   
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
        
    }

    public void StartClientTimer()
    {
        StartCoroutine(ClientTimer());
    }

    IEnumerator ClientTimer()
    {
        yield return new WaitForSeconds(clientTimer); // Espera 1 minuto
        if (!isAttended)
        {
            DestroyClient();
        }
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
