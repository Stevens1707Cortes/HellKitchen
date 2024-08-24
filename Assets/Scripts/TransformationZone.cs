using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransformationZone : MonoBehaviour
{
    [SerializeField] GameObject transformedObjectPrefab; // Prefab del objeto en el que quieres transformar
    [SerializeField] string targetName;
    [SerializeField] GameObject progressBar;
    [SerializeField] float transformationDelay = 3f;
    string targetTag = "Transformable";


    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que entra tiene el tag adecuado
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (collision.gameObject.GetComponent<Pickup>().foodName == targetName ) 
            {
                StartCoroutine(TransformObject(collision.gameObject));

                // Obtén la posición y rotación del objeto actual
                Vector3 position = collision.transform.position;
                Quaternion rotation = collision.transform.rotation;

                // Destruye el objeto actual
                // Destroy(other.gameObject);
                collision.gameObject.SetActive(false);

                // Instancia el nuevo objeto en la misma posición y rotación
                // Instantiate(transformedObjectPrefab, position, rotation);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag adecuado
        if (other.CompareTag(targetTag))
        {
            if (other.gameObject.GetComponent<Pickup>().foodName == targetName)
            {
                StartCoroutine(TransformObject(other.gameObject));

                // Obtén la posición y rotación del objeto actual
                Vector3 position = other.transform.position;
                Quaternion rotation = other.transform.rotation;

                // Destruye el objeto actual
                // Destroy(other.gameObject);
                other.gameObject.SetActive(false);

                // Instancia el nuevo objeto en la misma posición y rotación
                // Instantiate(transformedObjectPrefab, position, rotation);
            }
        }
    }
    private IEnumerator TransformObject(GameObject obj)
    {
        //Instanciarlo o dejarlo inactivo y activarlo durante la corutina
        //GameObject progressBar = Instantiate(progressBarPrefab, transform.position + Vector3.up * 2, transform.rotation);
        progressBar.SetActive(true);
        Slider slider = progressBar.GetComponent<Slider>();

        float elapsedTime = 0f;

        while (elapsedTime < transformationDelay)
        {
            elapsedTime += Time.deltaTime;
            slider.value = elapsedTime / transformationDelay;
            yield return null;
        }
        
        Instantiate(transformedObjectPrefab, obj.transform.position + new Vector3(0,0.5f,0), obj.transform.rotation);

        Destroy(obj);
        progressBar.SetActive(false);
        slider.value = 0;
        
        yield return new WaitForSeconds(1f);
        progressBar.SetActive(true);
    }

}
