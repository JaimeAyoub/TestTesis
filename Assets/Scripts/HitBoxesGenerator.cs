using System.Collections.Generic;
using UnityEngine;

public class HitBoxesGenerator : MonoBehaviour
{
    public GameObject[] leftArm;
    public GameObject[] rightArm;
    public GameObject[] leftLeg;
    public GameObject[] rightLeg;
    public List<GameObject> limbsChosen = new List<GameObject>();


    [SerializeField] private LayerMask layermask;

    public enum Limbs
    {
        leftArm,
        rightArm,
        leftLeg,
        rightLeg,
    }

    public Limbs _chooseLimbs;

    void Start()
    {
        layermask = LayerMask.GetMask("Enemy");
        limbsChosen.Clear();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Generate(Limbs limb)
    {
        switch (limb)
        {
            case Limbs.leftArm:
                if (leftArm.Length > 0)
                {
                    limbsChosen.Add(leftArm[0]);
                    limbsChosen.Add(leftArm[1]);
                }
                else
                {
                    Debug.Log("Brazo izquierdo vacio");
                }

                break;
            case Limbs.rightArm:
                if (rightArm.Length > 0)
                {
                    limbsChosen.Add(rightArm[0]);
                    limbsChosen.Add(rightArm[1]);
                }
                else
                {
                    Debug.Log("Brazo derecho vacio");
                }

                break;
            case Limbs.leftLeg:
                if (leftLeg.Length > 0)
                {
                    limbsChosen[0] = leftLeg[0];
                    limbsChosen[1] = leftLeg[1];
                }
                else
                {
                    Debug.Log("Pierna izquierda vacio");
                }

                break;
            case Limbs.rightLeg:
                if (rightLeg.Length > 0)
                {
                    limbsChosen[0] = rightLeg[0];
                    limbsChosen[1] = rightLeg[1];
                }
                else
                {
                    Debug.Log("Pierna derecha vacio");
                }

                break;
            default:
                break;
        }

        if (limbsChosen.Count > 0)
        {
            Collider[] hitColliders = Physics.OverlapCapsule(limbsChosen[0].transform.position,
                limbsChosen[1].transform.position, 0.5f, layermask);


            if (hitColliders.Length > 0)
            {
                CombatManager.instance.FreezeOnHit();
                CombatManager.instance.CameraShake();
                foreach (Collider collider in hitColliders)
                {
                    Debug.Log(collider.name);
                    if (collider.CompareTag("Enemy"))
                    {
                       // collider.GetComponent<EnemyHealth>().TakeDamage(1);
                       collider.GetComponent<EnemyInking>().AddInk(25.0f);
                    }
                }
            }

            limbsChosen.Clear();
        }
        else
        {
            Debug.Log("No hay partes seleccionadas");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(leftArm[0].transform.position, 0.50f);
        Gizmos.DrawSphere(leftArm[1].transform.position, 0.50f);

        Gizmos.DrawSphere(rightArm[0].transform.position, 0.50f);
        Gizmos.DrawSphere(rightArm[1].transform.position, 0.50f);
    }
}