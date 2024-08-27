using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int day;
    public int numberClients;
    public int numberBrain;
    public int numberKidney;
    public int numberHeart;

    private void Awake()
    {
        day = 1;
        numberClients = 3;
        numberBrain = 5;
        numberKidney = 5;
        numberHeart = 5;
    }

    private void Start()
    {
        
    }

    public void AddKidney()
    {
        numberKidney++;
    }

    public void RemoveKidney() { numberKidney--; }

    public void AddHeart() 
    { 
        numberHeart++;
    }

    public void RemoveHeart() { numberHeart--; }

    public void AddBrain()
    {
        numberBrain++;
    }

    public void RemoveBrain() { numberBrain--; }
}
