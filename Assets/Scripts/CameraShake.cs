
using UnityEngine;

public class CameraShake : MonoBehaviour
    /*Esta simula el movimiento de pasos y su movimiento en la camara*/
{
    public Transform player; 
    public float shakeAmount = 0.1f; 
    public float shakeSpeed = 2.5f; 
    public float heightAmplitude = 0.12f; 

    private Vector3 originalPosition;
    private Vector3 lastPlayerPosition;
    private float shakeTimer;

    void Start()
    {
        originalPosition = transform.localPosition; // Guardar la posición original de la cámara
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Calcular el desplazamiento del jugador
        Vector3 playerMovement = player.position - lastPlayerPosition;
        lastPlayerPosition = player.position;

        // Calcular el efecto de sacudida vertical
        if (playerMovement.magnitude > 0.01f) // Detectar movimiento del jugador
        {
            shakeTimer += Time.deltaTime * shakeSpeed;
        }
        else
        {
            shakeTimer = 0;
        }

        // Aplicar el efecto de sacudida vertical
        float shakeOffsetY = Mathf.Sin(shakeTimer * Mathf.PI * 2f) * heightAmplitude;

        // Aplicar el desplazamiento a la cámara
        transform.localPosition = originalPosition + new Vector3(0f, shakeOffsetY, 0f);
    }
}
