using UnityEngine;

public class E_Kamikaze : Enemy
{
    protected override void Start()
    {   
        base.Start();
        this.enemyName = "Kamikaze";

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
            this.TakeDamage(20);
        }
        
    }

}
