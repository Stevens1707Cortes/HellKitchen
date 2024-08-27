using UnityEngine;

public class IngredientLoot : MonoBehaviour
{
    [SerializeField] string nameIngredient;
    [SerializeField] private DungeonReset dungeonReset;

    private void Start()
    {
        dungeonReset = FindAnyObjectByType<DungeonReset>();
    }

    private void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (nameIngredient)
            {
                case "Kidney":
                    dungeonReset.AddKidney();
                    Destroy(gameObject);
                    break;
                case "Heart":
                    dungeonReset.AddHeart();
                    Destroy(gameObject);
                    break;
                case "Brain":
                    dungeonReset.AddBrain();
                    Destroy(gameObject);
                    break;
            }   
        }
    }
}
