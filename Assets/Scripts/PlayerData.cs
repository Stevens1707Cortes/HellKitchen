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
        numberClients = 5;
        numberBrain = 2;
        numberKidney = 2;
        numberHeart = 2;
    }

    private void Start()
    {
        
    }

    public void AddClient()
    {
        numberClients += 2;
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
