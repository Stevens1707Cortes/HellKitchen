using UnityEngine;

public class BrainPooling : IngredientPooling
{
    [SerializeField] private PlayerData playerData;
    protected override void Start()
    {
        maxActivations = playerData.numberBrain;
        base.Start();
    }
    public void UpdatePlayerData()
    {
        playerData.RemoveBrain();
    }
    private void OnEnable()
    {
        maxActivations = playerData.numberBrain;
        Initialize();
    }
}
