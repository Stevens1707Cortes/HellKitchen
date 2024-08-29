
using UnityEngine;

public class KitchenReset : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    private void OnEnable()
    {
        levelManager.isEndDay = false;
    }
    private void OnDisable()
    {
        levelManager.isKitchen = false;
        levelManager.ResetFood();
    }
}
