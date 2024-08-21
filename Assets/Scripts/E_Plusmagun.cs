using UnityEngine;

public class E_Plusmagun : Enemy
{
    [SerializeField] private float chaseRange = 15.0f; 
    [SerializeField] private float attackRange = 10.0f; // Rango de ataque del enemigo
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private float fireVelocity = 20f;
    [SerializeField] private BulletPooling bulletPooling;

    private float nextFireTime = 0f;

    protected override void Start()
    {
        base.Start();
        this.enemyName = "Plusmagun";
        bulletPooling.SetBulletsDamage(this.damage);
    }

    protected override void Update()
    {
        base.Update();

        if (isAttacking)
        {
            RotateTowardsPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.TakeDamage(20);
        }

    }

    protected override void ChasingBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, navMeshController.GetPlayer().position);

        if (distanceToPlayer > attackRange && distanceToPlayer <= chaseRange)
        {
            // Persigue al jugador
            navMeshController.MoveEnemy();
        }
        else if (distanceToPlayer <= attackRange)
        {
            // Cambia al estado de ataque si está dentro del rango de ataque
            SetState(State.Attacking);
        }
        else if (distanceToPlayer > chaseRange)
        {
            SetState(State.Returning);
        }
    }

    protected override void AttackingBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, navMeshController.GetPlayer().position);

        // Mantén la distancia del jugador y sigue disparando
        if (distanceToPlayer <= attackRange && distanceToPlayer > 0)
        {   
            isAttacking = true;

            navMeshController.StopAgent(); 

            enemyAnimator.SetBool("isAttacking", true);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (distanceToPlayer > attackRange)
        {   
            isAttacking = false;
            enemyAnimator.SetBool("isAttacking", false);
            SetState(State.Chasing);
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

}
