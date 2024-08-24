using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransformationZone : MonoBehaviour
{
    [SerializeField] GameObject transformedObjectPrefab; // Prefab del objeto en el que quieres transformar
    [SerializeField] string targetName;
    [SerializeField] GameObject progressBar;
    [SerializeField] float transformationDelay = 3f;
    [SerializeField] private IngredientPooling ingredientPooling; // Referencia al script de pooling
    string targetTag = "Transformable";


    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que entra tiene el tag adecuado
        if (collision.gameObject.CompareTag(targetTag) && collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            if (collision.gameObject.GetComponent<Pickup>().foodName == targetName)
            {
                collision.gameObject.SetActive(false);

                StartCoroutine(TransformObject(collision.gameObject));
            }

        }
    }

    private IEnumerator TransformObject(GameObject obj)
    {
        // Activa la barra de progreso
        progressBar.SetActive(true);
        Slider slider = progressBar.GetComponent<Slider>();

        float elapsedTime = 0f;

        while (elapsedTime < transformationDelay)
        {
            elapsedTime += Time.deltaTime;
            slider.value = elapsedTime / transformationDelay;
            yield return null;
        }

        // Desactiva el objeto original y lo devuelve al pool
        ingredientPooling.ActivateOneIngredient(obj);

        // Instancia el nuevo objeto transformado en la posición original del objeto
        Instantiate(transformedObjectPrefab, gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

        // Desactiva la barra de progreso y reinicia su valor
        progressBar.SetActive(false);
        slider.value = 0;

        yield return new WaitForSeconds(1f);

        // Rehabilita la barra de progreso para el próximo uso
        progressBar.SetActive(true);
    }

}
