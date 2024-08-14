using UnityEngine;

public class ShotGun : Gun
{
    private float nextFire = 0f;
    private Quaternion originalRotation;
    private bool isRecoiling = false; 


    [SerializeField] private float recoilRotationForce = 20f; //Angulo para rotar al disparar
    //[SerializeField] private Transform gunTransform;

    void Start()
    {   
        //Inicializar stats
        this.gunName = gameObject.name;
        this.currentAmmo = 12;
        this.maxAmmo = this.currentAmmo;
        this.fireRate = 1f;

        //Posiciones
        originalPosition = gunTransform.localPosition;
        originalRotation = gunTransform.localRotation;
    }

    void Update()
    {
        //Comprobacion de disparo
        if (Input.GetButton("Fire1") && Time.time >= nextFire) {
            if (currentAmmo > 0)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (currentAmmo == 0) {
                Reload();
            }
        }

        if (isRecoiling)
        {
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalPosition, Time.deltaTime * recoilSmoothness);
            gunTransform.localRotation = Quaternion.Slerp(gunTransform.localRotation, originalRotation, Time.deltaTime * recoilSmoothness);

            // Detener el retroceso cuando se ha recuperado completamente
            if (Vector3.Distance(gunTransform.localPosition, originalPosition) < 0.01f &&
                Quaternion.Angle(gunTransform.localRotation, originalRotation) < 0.01f)
            {   
                //Devolverlo a la posicion original
                gunTransform.localPosition = originalPosition; 
                gunTransform.localRotation = originalRotation; 
                isRecoiling = false;
            }
        }
    }

    void Recoil()
    {
        // Retroceso, y cambio de posición
        Vector3 recoilPosition = originalPosition + Vector3.back * recoilAmount;
        gunTransform.localPosition = recoilPosition;

        // Retroceso en rotación
        Quaternion recoilRotation = originalRotation * Quaternion.Euler(0f, recoilRotationForce, 0f); // Ángulo hacia arriba
        gunTransform.localRotation = recoilRotation;

        isRecoiling = true;
    }

    public override void Shoot()
    {
        base.Shoot();
        Recoil();
        currentAmmo--;
    }

    public override void Reload()
    {   
        base.Reload();
        currentAmmo = maxAmmo;
    }
}
