using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HashSet<GameObject> activeEnemies = new HashSet<GameObject>();
    [SerializeField] private int enemiesCount;

    void Start()
    {
        // Contar los enemigos iniciales en la escena
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            activeEnemies.Add(enemy);
        }

        enemiesCount = GetActiveEnemyCount();
    }

    private void OnEnable()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            activeEnemies.Add(enemy);
        }

        enemiesCount = GetActiveEnemyCount();
    }

    private void OnDisable()
    {
        activeEnemies.Clear();
        enemiesCount = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
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
