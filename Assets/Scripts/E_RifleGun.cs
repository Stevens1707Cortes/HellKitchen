using UnityEngine;

public class E_RifleGun : Enemy
{
    [SerializeField] private float attackRange = 20.0f; // Rango de ataque del enemigo
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private float fireVelocity = 20f;
    [SerializeField] private BulletPooling bulletPooling;

    private float nextFireTime = 0f;
    private bool isSetBulletDamage = false;

    protected override void Start()
    {
        base.Start();
        this.enemyName = "RifleGun";
        currentState = State.Idle;
        bulletPooling.SetEnemyBulletsDamage(this.damage);
    }

    protected override void Update()
    {
        base.Update();

        RotateTowardsPlayer();

        if (currentState == State.Idle && navMeshController.IsPlayerDetected())
        {
            SetState(State.Attacking);
        }

        if (!isSetBulletDamage)
        {
            bulletPooling.SetEnemyBulletsDamage(this.damage);
            isSetBulletDamage = true;
        }
    }

    protected override void IdleBehavior()
    {
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isDead", false);
        enemyAnimator.SetBool("isHitted", false);

        // Cambiar al estado Chasing si el jugador es detectado
        if (navMeshController.IsPlayerDetected())
        {
            SetState(State.Attacking);
        }
    }

    protected override void AttackingBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, navMeshController.GetPlayer().position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= nextFireTime)
            {
                enemyAnimator.SetBool("isAttacking", true);
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Actualiza el tiempo para el próximo disparo
            }
        }
        else
        {
            enemyAnimator.SetBool("isAttacking", false);
            SetState(State.Idle); 
        }
    }


    private void Shoot()
    {
        GameObject bullet = bulletPooling.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Hacer que la bala sea hija del enemigo
            bullet.transform.SetParent(this.transform);

            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * fireVelocity; // Asignar velocidad a la bala
        }
    }

    private void RotateTowardsPlayer()
    {
        // Obtiene la dirección hacia el jugador
        Vector3 directionToPlayer = (navMeshController.GetPlayer().position - transform.position).normalized;
        directionToPlayer.y = 0;

        // Calcula la rotación hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.TakeDamage(collision.gameObject.GetComponent<BulletController>().bulletDamage);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ArmAttack"))
        {
            this.TakeDamage(other.gameObject.GetComponentInParent<Arms>().armDamage);
        }
    }
}
