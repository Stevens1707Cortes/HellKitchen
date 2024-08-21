using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] string bulletName;
    public int bulletDamage;
    private BulletPooling bulletPooling;

    void Start()
    {
        bulletPooling = FindObjectOfType<BulletPooling>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bulletPooling.ReturnBulletToPool(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Cuando la bala sale de la pantalla, se regresa al pool
        bulletPooling.ReturnBulletToPool(gameObject);
    }

    
}
