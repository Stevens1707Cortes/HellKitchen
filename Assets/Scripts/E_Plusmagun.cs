using UnityEngine;

public class E_Plusmagun : Enemy
{
    protected override void Start()
    {
        base.Start();
        this.enemyName = "Plusmagun";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            this.TakeDamage(20);
        }

    }
}
