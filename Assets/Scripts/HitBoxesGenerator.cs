using UnityEngine;

public class HitBoxesGenerator : MonoBehaviour
{
    public GameObject foreArmLeft;
    public GameObject leftHand;

    [SerializeField]
    private LayerMask layermask;
    void Start()
    {
        layermask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(foreArmLeft.transform.position, leftHand.transform.position, 0.5f,layermask);

        foreach (Collider collider in hitColliders)
        {
            Debug.Log(collider.name);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(foreArmLeft.transform.position,0.5f);
        Gizmos.DrawSphere(leftHand.transform.position,0.5f);
    }
}
