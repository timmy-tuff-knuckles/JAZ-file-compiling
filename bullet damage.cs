//bullet damage
using system.collections;
using system.collections.generic;
using UnityEngine;
using UnityEngine.UI; 

public class Bullet : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
