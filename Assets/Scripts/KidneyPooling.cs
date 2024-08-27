
using UnityEngine;

public class KidneyPooling : IngredientPooling
{
    [SerializeField] private PlayerData playerData;
    protected override void Start()
    {
        maxActivations = playerData.numberKidney;
        base.Start();
    }

    public void UpdatePlayerData()
    {
        playerData.RemoveKidney();
    }

    private void OnEnable()
    {
        maxActivations = playerData.numberKidney;
        Initialize();
    }
}
