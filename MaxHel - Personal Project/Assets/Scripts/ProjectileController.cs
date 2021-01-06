using UnityEngine;

/* Projectile prefab supervisor
 * + track projectile position
 * + destroy projectile if meeting conditions
 */
public class ProjectileController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        } else if (other.CompareTag("Enemy") && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
            other.GetComponent<EnemyController>().lives--;
        } else if (other.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            other.GetComponent<PlayerController>().lives--;
        }
    }
}
