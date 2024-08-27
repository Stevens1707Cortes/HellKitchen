using UnityEngine;
using UnityEngine.AI;

public class DungeonReset : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    private GameObject player;
    private CharacterController characterController;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerData playerData;

    GameObject[] enemies;

    [SerializeField] private int kidneyCollected;
    [SerializeField] private int heartCollected;
    [SerializeField] private int brainCollected;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player");
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        kidneyCollected = 0;
        heartCollected = 0;
        brainCollected = 0;


        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();



        characterController.radius = 1.5f;
        characterController.height = 2f;

        playerController.enemyManager = enemyManager;

        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    private void OnDisable()
    {
        characterController = player.GetComponent<CharacterController>();

        playerController.enemyManager = null;
        characterController.radius = 0.5f;
        characterController.height = 2.5f;
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
