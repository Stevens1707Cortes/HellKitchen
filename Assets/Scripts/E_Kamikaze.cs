using UnityEngine;

public class E_Kamikaze : Enemy
{
    protected override void Start()
    {   
        base.Start();
        this.enemyName = "Kamikaze";

    }

    protected override void IdleBehavior()
    {
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isDead", false);
        enemyAnimator.SetBool("isHitted", false);

        // Cambiar al estado Chasing si el jugador es detectado
        if (navMeshController.IsPlayerDetected())
        {
            SetState(State.Chasing);
        }
    }

    public override void Die() 
    {
        base.Die();
        Debug.Log("Spawn Ingrediente");
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
