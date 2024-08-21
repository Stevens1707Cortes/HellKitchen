using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] private WeaponChange weaponSwitcher;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int armor;
    private int currentWeaponIndex = 0;
    private bool isDead = false;

    [Header("Player Config")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    private bool canSwitch;

    [Header("Weapons")]
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<Transform> weaponTransforms = new List<Transform>();

    [Header("Damage Settings")]
    [SerializeField] private float damageCooldown = 5f; 
    private bool canTakeDamage = true;
    [SerializeField] private EnemyManager enemyManager;

    [Header("Player Canvas Settings")]
    [SerializeField] private GameObject canvasDoor;
    [SerializeField] private TMP_Text lifeCanvas;

    private ClientManager clientManager;

    private Vector3 velocity;           
    private bool isGrounded;
    private bool isChangingWeapon = false;
    public bool isVictory = false;
    public bool isPaused = false;

    void Start()
    {
        //Comprobar escena de Dungeon, y habilitar el cambio de arma
        //canSwitch = GameManager.Instance.IsCurrentScene("StevenGym");
        canSwitch = true;

        //Configurar armas
        if (weapons.Count > 0)
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        }

        //Canvas

        if(canvasDoor != null)
        {
            canvasDoor.SetActive(false);
        }

        if (lifeCanvas != null)
        {
            lifeCanvas.text = "Life: " + maxHealth + " / " + health;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.HideAll();
            GameManager.Instance.TimeScale();
        }

        //Manager sobre el estado de clientes y enemigos

        enemyManager = FindObjectOfType<EnemyManager>();
        clientManager = FindObjectOfType<ClientManager>();
        
    }

    void Update()
    {
        // Inputs de Movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Mover el jugador en el eje Y
        controller.Move(velocity * Time.deltaTime);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Verificar si está en el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Actualizacion de la gravedad
        velocity.y += gravity * Time.deltaTime;

        //Cambio de arma
        if (canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isChangingWeapon)
            {
                StartCoroutine(ChangeWeapon());
            }
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            isPaused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GameManager.Instance.PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isPaused) 
        { 
            isPaused = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GameManager.Instance.ResumeGame();
        }

        //Comprobar condicion de victoria 1
        Victory();

        //Comprobar condicion de derrota 1
        GameOver();
    }

    private IEnumerator ChangeWeapon()
    {
        isChangingWeapon = true;

        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Configurar el WeaponSwitcher
        weaponSwitcher.Initialize(weaponTransforms[currentWeaponIndex], weaponTransforms[nextWeaponIndex]);
        yield return StartCoroutine(weaponSwitcher.SwitchWeapons());

        weapons[currentWeaponIndex].SetActive(false);
        weapons[nextWeaponIndex].SetActive(true);

        currentWeaponIndex = nextWeaponIndex;

        isChangingWeapon = false;
    }

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        lifeCanvas.text = "Life: " + maxHealth + " / " + health;
        if (health <= 0) 
        { 
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        isDead = true;
    }

    public void Victory()
    {
        if (enemyManager != null)
        {
            //Conteo de enemigos en el nivel
            if (enemyManager.GetActiveEnemyCount() <= 0)
            {
                isVictory = true;
                GameManager.Instance.Victory();
            }
        }

        if (clientManager != null)
        {
            if (clientManager.GetActiveClientCount() <= 0)
            {
                isVictory = true;
                GameManager.Instance.Victory();
            }
        }
    }

    public void GameOver()
    {
        if (isDead)
        {
            //Pantalla de derrota

            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator HandleDamageCooldown(int damage)
    {
        // Deshabilitar el daño mientras se espera el cooldown
        canTakeDamage = false; 
        PlayerTakeDamage(damage);
        Debug.Log("Enemigo colisionado. Vida restante: " + health);

        // Esperar para volver a recibir daño
        yield return new WaitForSeconds(damageCooldown); 
        canTakeDamage = true;
    }

    //Colisiones

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy") && canTakeDamage)
        {
            string name = hit.gameObject.GetComponent<Enemy>().enemyName;
            int damage = hit.gameObject.GetComponent<Enemy>().damage;

            switch (name)
            {
                case "Kamikaze":
                    StartCoroutine(HandleDamageCooldown(damage));
                    break;
                default:
                    StartCoroutine(HandleDamageCooldown(0));
                    break;
            }
        }

        if (hit.collider.CompareTag("MeeleAttack") && canTakeDamage)
        {
            StartCoroutine(HandleDamageCooldown(5));
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (canvasDoor != null)
            {
                canvasDoor.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pasando de nivel");
                GameManager.Instance.LoadScene("StevenGym");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (canvasDoor != null)
            {
                canvasDoor.SetActive(false);
            }

        }
    }
}
