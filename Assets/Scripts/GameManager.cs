using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiUXManager;
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
        uiUXManager = FindAnyObjectByType<UIManager>();
        HideAll();
    }

    void Update()
    {
        // L�gica para pausar el juego cuando es Game Over
        if (state == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }
    }

    public void HideAll()
    {
        uiUXManager.HideGameOver();
        uiUXManager.HideVictory();
    }

    //Cambiar a la escena de MainMenu
    public void LoadMainMenu()
    {
        uiUXManager.HideVictory();
        uiUXManager.HideGameOver();
        uiUXManager.ShowMainMenu();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        state = GameState.MainMenu;
    }

    // Funci�n para cambiar a la escena StevenGym
    public void LoadScene(string nameEscene)
    {
        uiUXManager.HideMainMenu();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameEscene);
        state = GameState.Playing;
    }

    //Pausar el juego
    public void PauseGame()
    {
        Time.timeScale = 0f;
        state = GameState.Paused;
    }

    //Reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        state = GameState.Playing;
    }

    //Reiniciar la escena actual
    public void RestartScene()
    {
        state = GameState.Playing;
        uiUXManager.HideGameOver();
        uiUXManager.HideVictory();
        uiUXManager.HideMainMenu();
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GameOver()
    {
        uiUXManager.ShowGameOver();
        //state = GameState.GameOver;
        Time.timeScale = 0f;
    }

    public void Victory()
    {
        uiUXManager.ShowVictory();
        Time.timeScale = 0f;
    }

    public void TimeScale() { Time.timeScale = 1f; }

}

