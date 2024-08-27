using UnityEngine;

public class Arms : MonoBehaviour
{   

    public int armDamage;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject collider1, collider2;

    private LevelManager levelManager;
    private float nextAttack = 0f; // Tiempo hasta el próximo ataque permitido
    public float attackRate = 0.5f; // Tiempo entre ataques
    private bool isRightHandAttack = true; // Comienza con el ataque de la mano derecha

    //Temporal

    private void Start()
    {
        collider1.SetActive(false);
        collider2.SetActive(false);
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void Update()
    {
        if (!levelManager.isKitchen && levelManager.isDungeon)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate;
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= nextAttack)
            {
                animator.SetBool("isTaking", true);
            }
            else
            {
                animator.SetBool("isTaking", false);
            }
        }

        
        
    }

    private void Attack()
    {
        if (isRightHandAttack)
        {
            collider2.SetActive(false);
            collider1.SetActive(true);
            // Reproduce la animación de ataque de la mano derecha
            animator.SetBool("isRightAttack", true);
            animator.SetBool("isLeftAttack", false);
        }
        else
        {
            collider1.SetActive(false);
            collider2.SetActive(true);
            // Reproduce la animación de ataque de la mano izquierda
            animator.SetBool("isLeftAttack", true);
            animator.SetBool("isRightAttack", false);
        }
        Invoke("DisableCollider", 0.2f);
        // Alterna la mano para el próximo ataque
        isRightHandAttack = !isRightHandAttack;
    }

    private void DisableCollider()
    {
        // Si no se está atacando, desactiva ambos colliders
        collider1.SetActive(false);
        collider2.SetActive(false);

        // Resetea las animaciones para que no estén en estado de ataque
        animator.SetBool("isRightAttack", false);
        animator.SetBool("isLeftAttack", false);
    }


}
