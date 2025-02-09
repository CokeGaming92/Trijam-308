using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player object to follow
    public float smoothSpeed = 5f; // Adjusts how smoothly the camera follows

    private Vector3 offset;

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position; // Store initial offset
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.y = transform.position.y; // Keep Y position unchanged
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}