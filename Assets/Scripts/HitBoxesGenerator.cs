using UnityEngine;

public class HitBoxesGenerator : MonoBehaviour
{
    public GameObject[] leftArm;
    public GameObject[] rightArm;
    public GameObject[] leftLeg;
    public GameObject[] rightLeg;


    [SerializeField] private LayerMask layermask;

    public enum Limbs
    {
        leftArm,
        rightArm,
        leftLeg,
        rightLeg,
    }

    Limbs _chooseLimbs;

    void Start()
    {
        layermask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Generate(Limbs limb)
    {
        GameObject[] limbsChosen = new GameObject[2];
        switch (_chooseLimbs)
        {
            case Limbs.leftArm:
                limbsChosen[0] = leftArm[0];
                limbsChosen[1] = leftArm[1];
                break;
            case Limbs.rightArm:
                limbsChosen[0] = rightArm[0];
                limbsChosen[1] = rightArm[1];
                break;
            case Limbs.leftLeg:
                limbsChosen[0] = leftLeg[0];
                limbsChosen[1] = leftLeg[1];
                break;
            case Limbs.rightLeg:
                limbsChosen[0] = rightLeg[0];
                limbsChosen[1] = rightLeg[1];
                break;
            default:
                break;
        }

        Collider[] hitColliders = Physics.OverlapCapsule(limbsChosen[0].transform.position,
            limbsChosen[1].transform.position, 0.50f, layermask);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                Debug.Log(collider.name);
            }
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