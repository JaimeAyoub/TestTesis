using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    public GameObject Player;

    private EnemyAttack enemyAttack;

    public bool isFollowing = false;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistances();
        setAnimations();

        if (Player != null)
        {
            if (isFollowing)
                m_Agent.SetDestination(Player.transform.position);
            else
                WaitForChasing();   

        }
    }


    void CheckDistances()
    {
        if (m_Agent.pathPending) return;

        isFollowing = m_Agent.remainingDistance > m_Agent.stoppingDistance;
    }

    void CheckAttack()
    {
        if (m_Agent.remainingDistance <= 2)
        {
            // Debug.Log("Ataque");
            enemyAttack.Attack();
        }
    }


    void setAnimations()
    {
        animator.SetBool("isWalking", isFollowing);
    }

    public async Awaitable WaitForChasing()
    {
        await Task.Delay((int)(2 * 1000));
        m_Agent.SetDestination(Player.transform.position);
        
    }
    
}