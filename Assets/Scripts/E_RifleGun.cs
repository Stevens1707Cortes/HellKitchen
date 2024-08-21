using UnityEngine;

public class E_RifleGun : Enemy
{
    protected override void Start()
    {
        base.Start();
        this.enemyName = "RifleGun";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.TakeDamage(20);
        }

    }

    private void QuitAttack()
    {
        enemyAnimator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
