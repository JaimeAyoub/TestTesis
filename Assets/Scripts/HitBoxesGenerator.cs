using System.Collections.Generic;
using UnityEngine;

public class HitBoxesGenerator : MonoBehaviour
{
    public GameObject[] leftArm;
    public GameObject[] rightArm;
    public GameObject[] leftLeg;
    public GameObject[] rightLeg;
    public List<GameObject> limbsChosen = new List<GameObject>();
    public Vector3 closestPoint;
    public Transform impactLimb;


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
        //layermask = LayerMask.GetMask("Enemy");
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
                Debug.Log(hitColliders.Length);

                foreach (Collider collider in hitColliders)
                {
                    impactLimb = limbsChosen[1].transform;


                    if (collider.CompareTag("Enemy") && this.gameObject.CompareTag("Player"))
                    {
                        // collider.GetComponent<EnemyHealth>().TakeDamage(1);
                        closestPoint = collider.ClosestPoint(impactLimb.position);
                        Vector3 resta = impactLimb.position - closestPoint;
                        resta.Normalize();
                        float angle = Vector3.Angle(collider.transform.forward, resta);
                        Debug.Log("Angulo: " + angle);

                        Vector3 rot = Vector3.RotateTowards(
                            impactLimb.position,
                            closestPoint,
                            1.0f * Time.deltaTime,
                            0.0f
                        );
                        CombatManager.instance.FreezeOnHit();
                        CombatManager.instance.CameraShake();
                        CombatManager.instance.SpawnHitVFX(closestPoint, rot.y);
                        GameManager.instance.AddCombo();
                        GameManager.instance.AddToTimer(0.5f);
                        collider.GetComponent<EnemyInking>().AddInk(25.0f * GameManager.instance.currentMultiplier);
                    }

                    if (collider.CompareTag("Player") &&  this.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("COLISION A PLAYER");
                        CombatManager.instance.FreezeOnHit();
                        CombatManager.instance.CameraShake();
                        collider.GetComponent<PlayerHealth>().TakeDamage(1);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(closestPoint, 0.1f);
        Gizmos.DrawWireSphere(closestPoint, 0.1f);
    }
}