using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HashSet<GameObject> activeEnemies = new HashSet<GameObject>();

    void Start()
    {
        // Contar los enemigos iniciales en la escena
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            activeEnemies.Add(enemy);
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public int GetActiveEnemyCount()
    {
        return activeEnemies.Count;
    }
}
