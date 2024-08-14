using System.Collections;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{

    /* Este codigo simula la animacion del cambio de armas. --No tocar-- */

    [Header("Weapon Config")]
    // Duración de la animación de cambio
    [SerializeField] private float switchDuration = 0.5f; 

    private Transform currentWeaponTransform;
    private Transform nextWeaponTransform;

    public void Initialize(Transform currentWeapon, Transform nextWeapon)
    {
        currentWeaponTransform = currentWeapon;
        nextWeaponTransform = nextWeapon;
    }

    //Corrutina para el cambio del arma
    public IEnumerator SwitchWeapons()
    {
        if (currentWeaponTransform == null || nextWeaponTransform == null)
            yield break;

        // Guardar las posiciones originales
        Vector3 currentWeaponStartPos = currentWeaponTransform.localPosition;
        Vector3 nextWeaponStartPos = nextWeaponTransform.localPosition;

        float elapsedTime = 0f;

        // Mueve el arma actual hacia abajo y el nuevo arma hacia arriba
        while (elapsedTime < switchDuration)
        {
            float interpolateTime = elapsedTime / switchDuration;
            currentWeaponTransform.localPosition = Vector3.Lerp(currentWeaponStartPos, currentWeaponStartPos + Vector3.down * 0.5f, interpolateTime);
            nextWeaponTransform.localPosition = Vector3.Lerp(nextWeaponStartPos, nextWeaponStartPos + Vector3.up * 0.5f, interpolateTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentWeaponTransform.localPosition = currentWeaponStartPos + Vector3.down * 0.5f;
        nextWeaponTransform.localPosition = nextWeaponStartPos + Vector3.up * 0.5f;

        elapsedTime = 0f;

        // Devolver las posiciones de las armas a las posiciones originales
        while (elapsedTime < switchDuration)
        {
            float interpolateTime = elapsedTime / switchDuration;
            currentWeaponTransform.localPosition = Vector3.Lerp(currentWeaponStartPos + Vector3.down * 0.5f, currentWeaponStartPos, interpolateTime);
            nextWeaponTransform.localPosition = Vector3.Lerp(nextWeaponStartPos + Vector3.up * 0.5f, nextWeaponStartPos, interpolateTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Las posiciones finales tienen que ser las originales
        currentWeaponTransform.localPosition = currentWeaponStartPos;
        nextWeaponTransform.localPosition = nextWeaponStartPos;
    }
}
