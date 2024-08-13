using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Sensibilidad del mouse
    public float mouseSensitivity = 100f;  
    public Transform playerBody;           

    private float xRotation = 0f;       

    void Start()
    {
        // Centrar el cursor
        Cursor.lockState = CursorLockMode.Locked;  
    }

    void Update()
    {
        // Input del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}