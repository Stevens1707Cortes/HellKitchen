
using UnityEngine;

public class KitchenReset : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private void OnDisable()
    {
        levelManager.isKitchen = false;
        levelManager.ResetFood();
    }
}
