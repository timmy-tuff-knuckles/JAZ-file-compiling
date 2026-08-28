using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimShoot : MonoBehaviour
{
    [SerializeField] public GameObject gun; // The gun object that will be rotated to aim at the mouse
    [SerializeField] public GameObject bullet; // The bullet prefab to be instantiated
    [SerializeField] private Transform bulletSpawnPoint; // The point from which the bullet will be spawned
    private GameObject bulletInst; // Reference to the instantiated bullet
    private Vector2 worldPosition; //World position of the mouse in world space
    private Vector2 direction; //Direction in world space for the gun to aim at
    private float angle; //Angle in world space for the gun to aim at
    public float bulletReload = 3f; // Time in seconds between shots

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting(); 
        bulletReload -= Time.deltaTime; // Decrease the reload timer by 1 every second
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

        // Flip the gun vertically if the angle is greater than 90 degrees or less than -90 degrees
        Vector3 localScale = new Vector3(0.45f, Mathf.Abs(localAngle) > 90f ? -0.45f : 0.45f, 2f);
        gun.transform.localScale = localScale; 
    }

    private void HandleGunShooting() // Handles shooting bullets when the left mouse button is pressed and the reload timer has elapsed
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && bulletReload <=0f)
        {
            // Use the world-space aim angle(Quaternion) to fix the bullet's rotation so it flies in the correct direction
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, bulletRotation);
            bulletReload = 3f; // Reset the reload timer
        }
    
    }
}
