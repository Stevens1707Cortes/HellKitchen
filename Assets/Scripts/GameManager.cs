using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiUXManager;

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float loadingDuration = 2f;

    public static GameManager Instance;
    public GameState state;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        MainMenu
    }

    //Estado de inicio
    void Start()
    {
        state = GameState.Playing;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.menuClip);
        //FindUIManager();
        HideAll();
    }

    void Update()
    {
        // L�gica para pausar el juego cuando es Game Over
        if (state == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (state == GameState.Playing)
            {
                PauseGame();
            }
            else if (state == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    private IEnumerator ShowLoadingScreen()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true); // Muestra la pantalla de carga

            yield return new WaitForSeconds(loadingDuration); // Espera la duración especificada

            loadingScreen.SetActive(false); // Oculta la pantalla de carga
        }
    }

    public void HideAll()
    {
        uiUXManager.HideGameOver();
        uiUXManager.HideVictory();
        uiUXManager.HidePauseMenu();
    }

    //Comprobar las escena actual
    public bool IsCurrentScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == sceneName;
    }

    //Cambiar a la escena de MainMenu
    public void LoadMainMenu()
    {
        StartCoroutine(ShowLoadingScreen());

        uiUXManager.HideVictory();
        uiUXManager.HideGameOver();
        uiUXManager.HidePauseMenu();
        uiUXManager.ShowMainMenu();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        state = GameState.MainMenu;
    }

    // Funci�n para cambiar a la escena 
    public void LoadScene(string nameEscene)
    {
        StartCoroutine(ShowLoadingScreen());

        if (nameEscene == "Main")
        {
            AudioManager.Instance.StopMusic();
            uiUXManager.HideMainMenu();
            uiUXManager.HidePauseMenu();
            Time.timeScale = 1f;
            SceneManager.LoadScene(nameEscene);
            state = GameState.Playing;
        } 
    }

    // Pausar el juego
    public void PauseGame()
    {
        state = GameState.Paused;
        Time.timeScale = 0f;
        uiUXManager.ShowPauseMenu();
        AudioManager.Instance.PauseMusic(); // Si tienes música que debe pausarse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Reanudar el juego
    public void ResumeGame()
    {
        state = GameState.Playing;
        Time.timeScale = 1f;
        uiUXManager.HidePauseMenu();
        AudioManager.Instance.ResumeMusic(); // Si tienes música que debe reanudarse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    //Reiniciar la escena actual
    public void RestartScene()
    {
        state = GameState.Playing;

        uiUXManager.HideGameOver();
        uiUXManager.HideVictory();
        uiUXManager.HideMainMenu();
        uiUXManager.HidePauseMenu();
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        uiUXManager.ShowGameOver();
        //state = GameState.GameOver;
        Time.timeScale = 0f;
    }

    public void Victory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        uiUXManager.ShowVictory();
        Time.timeScale = 0f;
    }

    public void TimeScale() { Time.timeScale = 1f; }


}

