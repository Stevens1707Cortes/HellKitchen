using UnityEngine;

public class E_Melee : Enemy
{
    [SerializeField] private float attackRange = 2.0f; // Rango de ataque del enemigo
    [SerializeField] private GameObject attackCollider;
    protected override void Start()
    {
        base.Start();
        this.enemyName = "Melee";
        attackCollider.SetActive(false);
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
        base.ChasingBehavior();

        // Verifica si el jugador está en rango de ataque
        if (navMeshController.IsPlayerInRange(attackRange))
        {
            SetState(State.Attacking);
        }
    }

    protected override void AttackingBehavior()
    {
        // Lógica para atacar al jugador
        if (navMeshController.IsPlayerInRange(attackRange)) 
        {
            if (!isAttacking) 
            {
                attackCollider.SetActive(true);
                isAttacking = true;
                enemyAnimator.SetBool("isAttacking", true);
                navMeshController.StopAgent();

                Invoke("QuitAttack", 1.5f);
            }
        }
        else
        {
            SetState(State.Chasing);
        }
    }

    private void QuitAttack()
    {
        attackCollider.SetActive(false);
        enemyAnimator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
