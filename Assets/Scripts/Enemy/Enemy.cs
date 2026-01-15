using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    public GameObject Player;

    private EnemyAttack enemyAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        CheckAttack();
          
    }

    void FollowPlayer()
    {
        if (Player && m_Agent.enabled)
        {
            m_Agent.SetDestination(Player.transform.position);
        }
       
    }

    void CheckAttack()
    {
        if (m_Agent.remainingDistance <= 2)
        {
           // Debug.Log("Ataque");
            enemyAttack.Attack();
        }
    }
}