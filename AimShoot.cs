using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimShoot : MonoBehaviour
{
    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject bullet; 
    [SerializeField] private Transform bulletSpawnPoint; 
    private GameObject bulletInst; 
    private Vector2 worldPosition; 
    private Vector2 direction; 
    private float angle; 

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting(); 
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // true world-space aim angle - helps when shooting bullets in the correct direction

        // local rotation visual flip
        Vector3 localTarget = gun.transform.parent.InverseTransformPoint(worldPosition);
        Vector2 localDir = ((Vector2)localTarget - (Vector2)gun.transform.localPosition).normalized;
        float localAngle = Mathf.Atan2(localDir.y, localDir.x) * Mathf.Rad2Deg;
        gun.transform.localRotation = Quaternion.Euler(0f, 0f, localAngle);

        Vector3 localScale = new Vector3(0.45f, Mathf.Abs(localAngle) > 90f ? -0.45f : 0.45f, 2f);
        gun.transform.localScale = localScale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Use the world-space aim angle(Quaternion) to fix the bullet's rotation so it flies in the correct direction
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, bulletRotation);
        }
    }
}
