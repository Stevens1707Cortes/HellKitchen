using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;           
    public float gravity = -9.81f;      
    public float jumpHeight = 3f;       

    private Vector3 velocity;           
    private bool isGrounded;          

    void Update()
    {
        // Verificar si está en el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movimiento en los ejes X y Z 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Actualizacion de la gravedad
        velocity.y += gravity * Time.deltaTime;

        // Mover el jugador en el eje Y
        controller.Move(velocity * Time.deltaTime);
    }
}
