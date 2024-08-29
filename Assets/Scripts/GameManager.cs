using System.Collections;
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
        uiUXManager.HideVictory();
        uiUXManager.HideGameOver();
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
            uiUXManager.HideMainMenu();
            Time.timeScale = 1f;
            SceneManager.LoadScene(nameEscene);
            state = GameState.Playing;
        } 
    }

    //Pausar el juego
    

    //Reanudar el juego
    

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

