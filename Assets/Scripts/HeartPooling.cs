using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPooling : IngredientPooling
{
    [SerializeField] private PlayerData playerData;
    protected override void Start()
    {
        maxActivations = playerData.numberHeart;
        base.Start();
    }
    public void UpdatePlayerData()
    {
        playerData.RemoveHeart();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        maxActivations = playerData.numberHeart;
        Initialize();
    }
}
