using UnityEngine;

public class PlasmaGun : Gun
{
    private float nextFire = 0f;
    private bool isRecoiling = false;
    private bool bulletUpdate = false; //Actualizar el dsño de la bala


    //[SerializeField] private Transform gunTransform;
    void Start()
    {
        //Inicializar stats
        this.gunName = gameObject.name;
        this.currentAmmo = 9;
        this.maxAmmo = this.currentAmmo;
        this.fireRate = 2f;

        //
        originalPosition = gunTransform.localPosition;
    }

    void Update()
    {

        if (!this.bulletUpdate)
        {
            //Inicializar el daño de las balas
            bulletPool.SetBulletsDamage(this.gunDamage);
            bulletUpdate = true;
        }

        //Comprobacion de disparo
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            if (currentAmmo > 0)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (currentAmmo == 0)
            {
                Reload();
            }
        }

        if (isRecoiling)
        {
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalPosition, Time.deltaTime * recoilSmoothness);

            // Detener el retroceso cuando se ha recuperado completamente
            if (Vector3.Distance(gunTransform.localPosition, originalPosition) < 0.01f)
            {
                gunTransform.localPosition = originalPosition; // Asegurar posición original
                isRecoiling = false;
            }
        }
    }

    void Recoil()
    {
        // Retroceso, y cambio de posicion
        gunTransform.localPosition = originalPosition + Vector3.back * recoilAmount;
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
