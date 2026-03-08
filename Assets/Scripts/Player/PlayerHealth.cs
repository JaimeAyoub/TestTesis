using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;

    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Death");
    }
}