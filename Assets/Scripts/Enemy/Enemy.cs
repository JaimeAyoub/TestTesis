using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (Player != null)
        {
                m_Agent.SetDestination(Player.transform.position);
            Debug.Log(m_Agent.remainingDistance);
            if (m_Agent.remainingDistance > 1.5f)
            {
                m_Agent.speed = 2.0f;
            }
            else
                m_Agent.speed = 0.0f;

           
        }
        else
            Debug.LogWarning("No hay player");
     
     
    }
}
