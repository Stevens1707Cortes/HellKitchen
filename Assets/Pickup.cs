using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{   
    public string foodName;
    public Transform pickupPoint;
    // Start is called before the first frame update
    void Start()
    {
        pickupPoint = GameObject.Find("Pickup Holder").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        transform.parent = pickupPoint.transform;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseUp() {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().freezeRotation = true;
    }
}
