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
        //rotate the gun towards the mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized; 
        gun.transform.right = direction; 

        //Flip the gun when when it reaches a 90° threshold 
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f); 
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f; 
        }
        else
        {
            localScale.y = 1f; 
        }

        gun.transform.localScale = localScale; 


    }

    

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //spawn bullet
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
    }
}
