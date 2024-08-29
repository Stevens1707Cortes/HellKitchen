using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas, defeatCanvas, controlCanvas, creditsCanvas, mainMenuCanvas;

    private void Start()
    {

        //GameManager.Instance.HideAll();
        if (controlCanvas != null) { controlCanvas.SetActive(false); }

        if (controlCanvas != null) { controlCanvas.SetActive(false); }

    }

    private void Update()
    {
        
    }

    public void ShowMainMenu()
    {
        if (mainMenuCanvas != null) { mainMenuCanvas.SetActive(true); }
    }

    public void HideMainMenu()
    {
        if (mainMenuCanvas != null) { mainMenuCanvas.SetActive(false); }
       
    }

    public void ShowControls()
    {
        if (controlCanvas != null) { controlCanvas.SetActive(true); }
    }

    public void HideControls()
    {
        if (controlCanvas != null) { controlCanvas.SetActive(false); }
    }

    public void ShowCredits()
    {
        if (creditsCanvas != null) { creditsCanvas.SetActive(true); }
    }

    public void HideCredits()
    {
        if (creditsCanvas != null) { creditsCanvas.SetActive(false); }
    }

    public void ShowVictory()
    {
        if (victoryCanvas != null) { victoryCanvas.SetActive(true); }
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
        if (defeatCanvas != null) {  defeatCanvas.SetActive(true); }
    }

    public void HideGameOver()
    {
        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(false);
        }
    }
}
