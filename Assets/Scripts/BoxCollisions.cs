using UnityEngine;
using UnityEngine.AI;

public class BoxCollisions : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {   
           //AddImpulse(other.gameObject);
            GameManager.instance.AddCombo();
        }
    }

    private void AddImpulse(GameObject target)
    {
        if (target)
        {
            target.GetComponent<NavMeshAgent>().enabled = false;
            target.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f,20.0f,0.0f), ForceMode.Impulse);
            Debug.Log("AddImpulse");
        }
        else 
            Debug.Log("No target");
    }
}
