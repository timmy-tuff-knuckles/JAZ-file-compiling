using UnityEngine;

public class NoFlip : MonoBehaviour
{
    private Transform mainCameraTransform; // Reference to the main camera's transform
    private Vector3 originalLocalScale; // Store the original local scale of the object

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        originalLocalScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // Face the camera
            transform.LookAt(transform.position + mainCameraTransform.forward);
        }

        // Counteract any negative scale inherited from the flipped parent 
        if (transform.parent != null)
        {
            Vector3 parentScale = transform.parent.lossyScale;

            transform.localScale = new Vector3(
                originalLocalScale.x * Mathf.Sign(parentScale.x),
                originalLocalScale.y * Mathf.Sign(parentScale.y),
                originalLocalScale.z * Mathf.Sign(parentScale.z)
            );
        }
    }
}