using UnityEditorInternal;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 moveVector;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {


        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = moveVector * speed;


        
       
    }
}
