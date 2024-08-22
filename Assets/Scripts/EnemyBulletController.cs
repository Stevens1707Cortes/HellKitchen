using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public string bulletName;
    public int bulletDamage;
    private BulletPooling bulletPooling;

    void Start()
    {
        bulletPooling = FindObjectOfType<BulletPooling>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Player"))
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
