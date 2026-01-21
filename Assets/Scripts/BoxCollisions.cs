using UnityEngine;
using UnityEngine.AI;

public class BoxCollisions : MonoBehaviour
{
    private float timeToTurnOff = 0.5f;
    private float timeTurnOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //AddImpulse(other.gameObject);
            GameManager.instance.AddCombo();
        }

        if (other.CompareTag("Parry"))
        {
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
            if (player != null)
            {
                player.GetComponent<Rigidbody>().AddForce(player.transform.forward.x, player.transform.forward.y,
                    player.transform.forward.z * -220.0f, ForceMode.Impulse);
            }

            Debug.Log("PARRYYYY!");
        }
    }

    private void AddImpulse(GameObject target)
    {
        if (target)
        {
            target.GetComponent<NavMeshAgent>().enabled = false;
            target.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode.Impulse);
            Debug.Log("AddImpulse");
        }
        else
            Debug.Log("No target");
    }

    void CheckIfActive()
    {
        if (!gameObject.activeSelf) return;
        timeTurnOn += Time.deltaTime;
        if (timeTurnOn > timeToTurnOff)
        {
            timeTurnOn = 0.0f;
            gameObject.SetActive(false);
        }
    }
}