using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float speed = 5f; // Public speed variable
    private Rigidbody rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement magnitude (without multiplying by deltaTime)
        float movementSpeed = new Vector3(moveX, 0f, moveZ).magnitude;

        // Set the Animator parameter
        animator.SetFloat("Speed", movementSpeed > 0 ? 5f : 0f);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Apply movement with speed and deltaTime in FixedUpdate
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }
}
