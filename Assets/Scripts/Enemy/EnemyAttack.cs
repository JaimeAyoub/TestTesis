using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private Animator anim;
    public bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>(); // ← Cachear, no buscar cada frame
    }

    public void Attack()
    {
        if (isAttacking) return; // ← Si ya está atacando, ignorar
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);

        // Esperar que termine la animación de ataque
        yield return new WaitUntil(() => 
            anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")); // ← Nombre de tu estado en el Animator
        yield return new WaitUntil(() => 
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }
}