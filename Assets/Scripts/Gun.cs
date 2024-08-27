using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public string gunName; // Nombre del arma
    public int currentAmmo; // Municion
    public int maxAmmo; // Municion Maxima
    public int gunDamage; // Municion Maxima
    [SerializeField] protected Transform gunTransform;
    protected Vector3 originalPosition;

    [Header("Gun Config")]
    public float fireRate;
    protected bool isReloading = false;
    protected float timeReload = 2f; // Tiempo de recargar
    [SerializeField] protected float recoilAmount = 0.1f;  // Fuerza del retroceso
    [SerializeField] protected float recoilSpeed = 10f;  // Velocidad de recuperacion del retroceso
    [SerializeField] protected float recoilSmoothness = 5f;

    [Header("Shoot Options")]
    public BulletPooling bulletPool;
    public Transform firePoint;
    public RectTransform sightCanvas;

    [Header("Muzzle Flash")]
    public ParticleSystem muzzleFlash; // Sistema de part�culas del muzzle flash

    public virtual void Shoot()
    {

        // Convertir la posici�n de la mira en un rayo en el mundo 3D
        Ray ray = Camera.main.ScreenPointToRay(sightCanvas.position);
        RaycastHit hit;

        // Direcci�n hacia donde disparar
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Si el rayo golpea algo, dispara hacia ese punto
            targetPoint = hit.point;
        }
        else
        {
            // Si no golpea nada, dispara en la direcci�n del rayo a una distancia lejana
            targetPoint = ray.GetPoint(1000f);
        }

        // Calcular la direcci�n desde el punto de disparo hacia el objetivo
        Vector3 direction = targetPoint - firePoint.position;

        //Animacion de disparo
        if (muzzleFlash != null)
        {
            // Reproducir el sistema de part�culas
            muzzleFlash.Play();

            // Programar la detenci�n del sistema de part�culas despu�s de su duraci�n
            Invoke("StopMuzzleFlash", muzzleFlash.main.duration);
        }

        GameObject bullet = bulletPool.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.SetActive(true);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * 60f; // Ajusta la velocidad seg�n sea necesario
            }
        }

    }

    protected void StopMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public virtual void Reload()
    {
        Debug.Log("Recargando...." + gunName);
    }
}
