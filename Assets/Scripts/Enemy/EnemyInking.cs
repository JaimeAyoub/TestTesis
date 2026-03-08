using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;


public class EnemyInking : MonoBehaviour
{
    public float amountOfInk = 0.0f;
    public float inkForDeath = 100.0f;
    public VisualEffect deathEffect;
    private Animator anim;
    //Aqui tendira que pasarle el shader y qye el amount of ink sea el que diga cuanto "manche" al enemigo.


    void Start()
    {
        deathEffect = GetComponent<VisualEffect>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddInk(float inkToAdd)
    {
        if (amountOfInk < inkForDeath)
        {
            anim.Play("Hit Reaction", -1, 0f);

            amountOfInk += inkToAdd;
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer)
            {
                skinnedMeshRenderer.material.SetFloat("_AmountOfInk", (amountOfInk / inkForDeath));
            }

            if (amountOfInk >= inkForDeath)
            {
                InkDeath();
                // deathEffect.Play();
            }
        }
    }

    void InkDeath()
    {
        GameManager.instance.SpawnInkDeath(this.gameObject);
        GameManager.instance.SpawnEnemy();
    }
}