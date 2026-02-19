using UnityEngine;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 500;
    public int currentHealth = 0;
    private Tween damageTween;
    private Animator anim;


    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            anim.SetTrigger("HitTrigger");
            if (damageTween != null && damageTween.IsPlaying())
            {
                damageTween.Kill();
            }

            // damageTween = this.gameObject.transform.DOShakeRotation(0.5f, 10.0f);

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        this.transform.DOScale(Vector3.zero, 0.05f).OnComplete(() => Destroy(this.gameObject));
    }
}