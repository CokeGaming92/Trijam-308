using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float Speed = 5f;   // Maximum speed (Run speed)
    public float WalkSpeed = 2.5f;  // Walking speed
    public float RotationSpeed = 10f;
    public Transform CameraTransform;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float movementMagnitude = direction.magnitude; // Get movement intensity (0 to 1)

        if (movementMagnitude >= 0.1f)
        {
            // Rotate character based on movement direction + camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref RotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the character forward
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Speed : WalkSpeed; // Shift to run
            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }

        // Apply gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f; // Keep the player grounded
        }

        controller.Move(velocity * Time.deltaTime);

        // Set Animator Speed Parameter
        if (animator != null)
        {
            float animationSpeed = movementMagnitude * (Input.GetKey(KeyCode.LeftShift) ? Speed : WalkSpeed);
            animator.SetFloat("Speed", animationSpeed, 0.1f, Time.deltaTime); // Smooth transition
        }
    }
}
