using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float attackCooldown = 0.5f;
    private float attackOffCooldown = 1.0f;
    private float attackTime;
    public bool isAttacking = false;

 

    public BoxCollider attackCollider;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        if((Time.time - attackTime) > attackCooldown && !isAttacking)
        {
            attackCollider.gameObject.SetActive(true);
            attackTime = Time.time;
            isAttacking = true;
          
         
        }
        
        if(isAttacking && (Time.time - attackTime) > attackOffCooldown)
        {
            attackCollider.gameObject.SetActive(false);
            attackTime = Time.time;
            isAttacking= false;
        }
    }

    

   
}
