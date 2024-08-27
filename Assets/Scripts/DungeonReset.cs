using UnityEngine;

public class DungeonReset : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerData playerData;

    [SerializeField] private int kidneyCollected;
    [SerializeField] private int heartCollected;
    [SerializeField] private int brainCollected;


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
        playerData.AddBrain();
    }

    public void AddHeart()
    {
        heartCollected++;
        playerData.AddHeart();
    }

    public void AddKidney()
    {
        kidneyCollected++;
        playerData.AddKidney();
    }
}
