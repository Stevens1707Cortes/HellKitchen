using UnityEngine;

public class E_Melee : Enemy
{
    protected override void Start()
    {
        base.Start();
        this.enemyName = "Melee";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.TakeDamage(20);
        }

    }
}
