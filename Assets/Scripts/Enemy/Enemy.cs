using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public GameObject Player;
    private EnemyAttack enemyAttack;
    public bool isFollowing = false;
    public Animator animator;

    private bool isWaiting = false; // ← Bandera para no spamear corrutinas

    public float attackDistance;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Player == null) return;

        CheckDistances();
        SetAnimations();
        CheckAttack();
        gameObject.transform.LookAt(Player.transform);

        if (isFollowing)
        {
            isWaiting = false; // Si empieza a seguir, cancelamos estado de espera
            m_Agent.SetDestination(Player.transform.position);
        }
        else if (!isWaiting) // ← Solo lanza la corrutina si no hay una activa
        {
            StartCoroutine(WaitThenChase());
        }
    }

    void CheckDistances()
    {
        if (m_Agent.pathPending) return;

        isFollowing = m_Agent.remainingDistance > m_Agent.stoppingDistance
                      && m_Agent.remainingDistance > attackDistance;
    }

    void CheckAttack()
    {
        if (m_Agent.pathPending) return;

       // Debug.Log(m_Agent.remainingDistance);
        if (m_Agent.remainingDistance <= attackDistance)
            enemyAttack.Attack();
        else
        {
            EnemyAttack enemyAttack = GetComponent<EnemyAttack>();
            enemyAttack.isAttacking =  false;
        }
    }

    void SetAnimations()
    {
        animator.SetBool("isWalking", isFollowing);
    }

    IEnumerator WaitThenChase()
    {
        isWaiting = true; // Bloquear nuevas corrutinas
        yield return new WaitForSeconds(2f); // Esperar en el main thread (seguro)

        if (Player != null)
            m_Agent.SetDestination(Player.transform.position);

        isWaiting = false; // Liberar para la próxima vez
    }
}