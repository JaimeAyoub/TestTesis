using UnityEngine;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 10;
    public int currentHealth = 0;
    private Tween damageTween;

    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            if (damageTween != null && damageTween.IsPlaying())
            {
                damageTween.Kill();
            }

            damageTween = this.gameObject.transform.DOShakeRotation(0.5f, 10.0f);

            currentHealth -= damage;
        }
        else
        {
            Death();
        }
    }

    void Death()
    {
        this.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => Destroy(this.gameObject));
    }
}