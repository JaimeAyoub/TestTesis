using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;


public class EnemyInking : MonoBehaviour
{
    public float amountOfInk = 0.0f;
    private float _inkForDeath = 50.0f;
    public VisualEffect deathEffect;
    //Aqui tendira que pasarle el shader y qye el amount of ink sea el que diga cuanto "manche" al enemigo.


    void Start()
    {
        deathEffect = GetComponent<VisualEffect>();
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
            GetComponent<MeshRenderer>().material.SetFloat("_Radius", amountOfInk/_inkForDeath);
            if (amountOfInk >= _inkForDeath)
            {
                InkDeath();
            }
        }
    }

    void InkDeath()
    {

     
       s deathEffect.SendEvent("OnPlay");

        this.transform.DOScale(Vector3.zero, 0.3f);






    }
}