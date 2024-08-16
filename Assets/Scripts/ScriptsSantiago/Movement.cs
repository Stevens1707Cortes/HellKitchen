using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 move;
    public float speed;
    public float jump;
    public Transform orientation;
    private float vertical;
    private float horizontal;
    //public float downwardForce;
    //public float downwardMultiplier;
    public LayerMask Ground;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        move = orientation.right * horizontal + orientation.forward * vertical;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping();
        }

        // if (isGrounded())
        // {
        //     downwardForce = 0;
        // }
        // else
        // {
        //     downwardForce += Time.deltaTime * downwardMultiplier;
        //     rb.AddForce(-transform.up * downwardForce, ForceMode.Acceleration);
        // }

    }

    private void FixedUpdate() {
        rb.AddForce(move.normalized * speed, ForceMode.Acceleration);
    }

    public void jumping()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jump, ForceMode.Impulse); 
    }

    // public bool isGrounded()
    // {
    //     RaycastHit hit;
    //     if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, Ground))
    //     {
    //         return true;
    //     }
    //     else 
    //     {
    //         return false;
    //     }
    // }
}
