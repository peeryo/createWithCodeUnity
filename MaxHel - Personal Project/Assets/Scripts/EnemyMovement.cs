using UnityEngine;

/* Enemy movement class script
 * + control how the enemy can move in relation with player position
 * + control the movement speed of enemy tanks
 */
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float turnSpeed;
    
    private Transform player;
    private Vector3 lookRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection).eulerAngles - transform.rotation.eulerAngles;

        if (distance >= 40)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (lookDirection), Time.deltaTime * turnSpeed);
        } else if (!((lookRotation.y <= 70 && lookRotation.y >= 0) ||
                     (lookRotation.y >= 290 && lookRotation.y <= 360) ||
                     (lookRotation.y <= 0 && lookRotation.y >= -70)))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime);
        }
    }
}
