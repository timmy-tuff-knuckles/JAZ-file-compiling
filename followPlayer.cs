using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // Drag your player here
    [SerializeField] private float smoothTime = 0.3f; // Delay time to catch up
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Keep camera at Z = -10

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        // Define our target destination
        Vector3 targetPosition = target.position + offset;

        // Smoothly glide from current camera position to target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
