using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class EnemyInking : MonoBehaviour
{
    public float amountOfInk = 0.0f;
    private float _inkForDeath;
    public VisualEffectAsset deathEffect;
    //Aqui tendira que pasarle el shader y qye el amount of ink sea el que diga cuanto "manche" al enemigo.


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void AddInk(float inkToAdd)
    {
        if (amountOfInk < _inkForDeath)
        {
            amountOfInk += inkToAdd;
            if (amountOfInk >= _inkForDeath)
            {
                InkDeath();
            }
        }
    }

    void InkDeath()
    {
        
    }
}