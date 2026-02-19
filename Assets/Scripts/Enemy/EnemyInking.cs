using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;


public class EnemyInking : MonoBehaviour
{
    public float amountOfInk = 0.0f;
    private float _inkForDeath = 50.0f;
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
        if (amountOfInk < _inkForDeath)
        {
            anim.SetTrigger("HitTrigger");
            amountOfInk += inkToAdd;
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer)
            {
                skinnedMeshRenderer.material.SetFloat("_AmountOfInk", (amountOfInk / _inkForDeath)/100);
            }

            if (amountOfInk >= _inkForDeath)
            {
                InkDeath();
                deathEffect.Play();
            }
        }
    }

    void InkDeath()
    {
            Sequence s = DOTween.Sequence();
            s.Append(this.transform.DOScale(Vector3.zero, 1.0f))
                .AppendCallback(() => deathEffect.SendEvent("OnPlay"))
                .OnComplete(() => Destroy(this.gameObject));

      
    }
}