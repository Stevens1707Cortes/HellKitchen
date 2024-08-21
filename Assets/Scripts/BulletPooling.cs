using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 3;
    public List<GameObject> bulletList = new List<GameObject>();
    void Start()
    {
        InstantiateBullets(poolSize);
    }

    void InstantiateBullets(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
        }
    }

    public GameObject GetPooledBullet()
    {
        foreach (GameObject bullet in bulletList)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    public void SetBulletsDamage(int damage)
    {
        foreach (GameObject bullet in bulletList)
        {
            bullet.GetComponent<EnemyBulletController>().bulletDamage = damage;
        }
    }

    // Método para regresar la bala al pool
    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
