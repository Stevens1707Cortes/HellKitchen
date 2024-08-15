using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas, defeatCanvas, controlCanvas, creditsCanvas, mainMenuCanvas;


    private void Start()
    {

        //GameManager.Instance.HideAll();
        controlCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
    }

    public void HideMainMenu()
    {
        mainMenuCanvas.SetActive(false);
    }

    public void ShowControls()
    {
        controlCanvas.SetActive(true);
    }

    public void HideControls()
    {
        controlCanvas.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsCanvas.SetActive(true);
    }

    public void HideCredits()
    {
        creditsCanvas.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryCanvas.SetActive(true);
    }

    public void HideVictory()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(false);
        }
    }
    public void ShowGameOver()
    {
        defeatCanvas.SetActive(true);
    }

    public void HideGameOver()
    {
        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(false);
        }
    }
}
