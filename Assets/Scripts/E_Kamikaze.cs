using UnityEngine;

public class E_Kamikaze : Enemy
{
    protected override void Start()
    {   
        base.Start();
        this.enemyName = "Kamikaze";

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) 
        {
            this.TakeDamage(20);
        }
        
    }

}
