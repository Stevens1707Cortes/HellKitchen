using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    public string enemyName;
    [SerializeField] protected Animator enemyAnimator;

    [SerializeField] protected EnemyManager enemyManager;
    [SerializeField] protected NavMeshController navMeshController;

    //Maquina de estados
    protected enum State { Idle, Chasing, Attacking, Returning, Dead };
    [SerializeField] protected State currentState;

    protected virtual void Start()
    {
        navMeshController = gameObject.GetComponent<NavMeshController>();
        enemyAnimator = GetComponent<Animator>();
        currentState = State.Idle;

        //Conteo de enemigos
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.RegisterEnemy(gameObject);
    }

    protected virtual void Update()
    {
        switch (currentState) 
        { 
            case State.Idle:
                IdleBehavior();
                break;
            case State.Chasing:
                ChasingBehavior();
                break;
            case State.Attacking:
                AttackingBehavior();
                break;
            case State.Returning: 
                ReturningBehavior();
                break;
            case State.Dead:
                Die();
                break;

        }

        // Actualizar la animación en función de la velocidad del agente
        if (navMeshController.IsMoving())
        {
            enemyAnimator.SetBool("isMoving", true);
        }
        else
        {
            enemyAnimator.SetBool("isMoving", false);
        }
    }


    // Métodos que definen el comportamiento en cada estado
    protected virtual void IdleBehavior()
    {   
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isDead", false);
        enemyAnimator.SetBool("isHitted", false);

        // Cambiar al estado Chasing si el jugador es detectado
        if (navMeshController.IsPlayerDetected())
        {
            SetState(State.Chasing);
        }
    }

    protected virtual void ChasingBehavior()
    {
        navMeshController.MoveEnemy();

        // Cambiar al estado Returning si el jugador ya no es detectado
        if (!navMeshController.IsPlayerDetected())
        {
            SetState(State.Returning);
        }
    }

    protected virtual void AttackingBehavior()
    {
        // Lógica para atacar al jugador
    }

    protected virtual void ReturningBehavior()
    {
        navMeshController.ReturnOriginalPosition();

        // Cambiar al estado Idle si el enemigo ha vuelto a la posición original
        if (navMeshController.HasReturnedToOriginalPosition())
        {
            SetState(State.Idle);
        }
    }

    // Método para cambiar de estado
    protected void SetState(State newState)
    {
        currentState = newState;
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0) 
        {
            SetState(State.Dead);
        }
    }
    public virtual void Die() 
    {
        enemyManager.UnregisterEnemy(gameObject);
        navMeshController.StopAgent();
        enemyAnimator.SetBool("isDead", true);
        Invoke("DisableEnemy",3f);
    }

    public virtual void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

}
