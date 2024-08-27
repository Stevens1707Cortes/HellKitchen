using UnityEngine;

public class DungeonReset : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] PlayerController playerController;

    private int kidneyCollected;
    private int heartCollected;
    private int brainCollected;


    private void OnEnable()
    {
        kidneyCollected = 0;
        heartCollected = 0;
        brainCollected = 0;

        playerController = FindObjectOfType<PlayerController>();

        playerController.enemyManager = enemyManager;
    }

    private void OnDisable()
    {
        playerController.enemyManager = null;
    }

    public void AddBrain()
    {
        brainCollected++;
    }

    public void AddHeart()
    {
        heartCollected++;
    }

    public void AddKidney()
    {
        kidneyCollected++;
    }
}
