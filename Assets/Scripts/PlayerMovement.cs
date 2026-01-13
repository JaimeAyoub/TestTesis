using UnityEditorInternal;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public Vector3 moveVector;
    public float speed;

    public float targetAngle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
    }

    void MoveAndRotate()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");
        _rb.linearVelocity = moveVector * speed;

        if (moveVector == Vector3.zero) return;
        targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}