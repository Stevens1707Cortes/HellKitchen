using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerController : MonoBehaviour
{
    //public CharacterController controller;
    //[SerializeField] private WeaponChange weaponSwitcher;

    //[Header("Player Stats")]
    //[SerializeField] private int health = 100;
    //[SerializeField] private int armor = 100;
    //private int currentWeaponIndex = 0;

    //[Header("Player Config")]
    //[SerializeField] private float speed = 12f;
    //[SerializeField] private float gravity = -9.81f;
    //[SerializeField] private float jumpHeight = 3f;

    //[Header("Weapons")]
    //[SerializeField] private List<GameObject> weapons = new List<GameObject>();
    //[SerializeField] private List<Transform> weaponTransforms = new List<Transform>();


    //private Vector3 velocity;           
    //private bool isGrounded;
    //private bool isChangingWeapon = false;

    //void Start()
    //{   
    //    if (weapons.Count > 0)
    //    {
    //        weapons[0].SetActive(true);
    //        weapons[1].SetActive(false);
    //    } 
    //}

    //void Update()
    //{
    //    // Inputs de Movimiento
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    Vector3 move = transform.right * x + transform.forward * z;
    //    controller.Move(move * speed * Time.deltaTime);

    //    // Mover el jugador en el eje Y
    //    controller.Move(velocity * Time.deltaTime);

    //    // Salto
    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    }

    //    // Verificar si está en el suelo
    //    isGrounded = controller.isGrounded;
    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f;
    //    }

    //    // Actualizacion de la gravedad
    //    velocity.y += gravity * Time.deltaTime;

    //    //Cambio de arma
    //    //if (Input.GetKeyDown(KeyCode.Q))
    //    //{
    //    //    ChangeWeapon();
    //    //}

    //    if (Input.GetKeyDown(KeyCode.Q) && !isChangingWeapon)
    //    {
    //        StartCoroutine(ChangeWeapon());
    //    }

    //}

    //private IEnumerator ChangeWeapon()
    //{
    //    isChangingWeapon = true;

    //    int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

    //    // Configurar el WeaponSwitcher
    //    weaponSwitcher.Initialize(weaponTransforms[currentWeaponIndex], weaponTransforms[nextWeaponIndex]);
    //    yield return StartCoroutine(weaponSwitcher.SwitchWeapons());

    //    weapons[currentWeaponIndex].SetActive(false);
    //    weapons[nextWeaponIndex].SetActive(true);

    //    currentWeaponIndex = nextWeaponIndex;

    //    isChangingWeapon = false;
    //}

    //private void ChangeWeapon()
    //{
    //    weapons[currentWeaponIndex].SetActive(false);

    //    //Usando el modulo, incrementamos el indice y reinice cuando llegue al final de la lista
    //    currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

    //    weapons[currentWeaponIndex].SetActive(true);
    //}
}
