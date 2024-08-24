using UnityEngine;

public class Pickup : MonoBehaviour
{   
    public string foodName;
    [SerializeField] Transform pickupPoint;
    private int originalLayer;
    [SerializeField] int dinnerLayer;

    private IngredientPooling poolingComponent;


    void Start()
    {
        pickupPoint = GameObject.Find("PSX_Arms").GetComponent<Transform>();
        originalLayer = gameObject.layer;
        dinnerLayer = GameObject.Find("DINER").layer;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetPoolingComponent(IngredientPooling pooling)
    {
        poolingComponent = pooling;
    }

    void OnDisable()
    {
        if (poolingComponent != null)
        {
            poolingComponent.ReturnIngredientToPool(gameObject);
        }
    }

    private void OnMouseDown() {
        transform.parent = pickupPoint.transform;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        //GetComponent<BoxCollider>().enabled = false;
        gameObject.layer = dinnerLayer;
    }

    private void OnMouseUp() {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        gameObject.layer = originalLayer;
    }
}
