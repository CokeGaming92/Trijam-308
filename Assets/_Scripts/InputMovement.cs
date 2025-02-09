using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float speed = 5f; // Public speed variable
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }
}