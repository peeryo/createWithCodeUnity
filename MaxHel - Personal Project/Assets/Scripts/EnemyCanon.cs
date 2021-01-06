using UnityEngine;

/* Enemy cannon class script
 * + control how and when the enemy fire bullets to the player
 * + control the bullet launch force of the tank
 * + control how the enemy can target or aimed in relation with the player position
 */
public class EnemyCanon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float impulsionForce; 
    
    [SerializeField] private float rotationYLimit;
    [SerializeField] private float rotationXMin;
    [SerializeField] private float rotationXMax;

    private Transform cannonTower;
    private Transform cannonGun;
    private Transform player;
    private Vector3 lookRotation;
    private float rechargeTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        cannonTower = transform.GetChild(0);
        cannonGun = cannonTower.GetChild(0);
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Little IA using distance-based strategies to attack the player
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection).eulerAngles - transform.rotation.eulerAngles;

        if (distance < 40)
        {
            if ((lookRotation.y <= 70 && lookRotation.y >= 0) || 
                (lookRotation.y >= 290 && lookRotation.y <= 360) || 
                (lookRotation.y <= 0 && lookRotation.y >= -70))
            {
                RotateCannonTower();
            }
        }
        
        float rotationX = (50.0f / 80.0f) * distance;
        RotationCannonGun(rotationX);
    }

    // FixedUpdate is called once per frame before Update
    private void FixedUpdate()
    {
        if (rechargeTime <= 0)
        {
            CannonShoot();
        }

        rechargeTime -= Time.deltaTime;
    }

    // Control the cannon tower rotation
    private void RotateCannonTower()
    {
        float rotationY = Mathf.Clamp(lookRotation.y, -rotationYLimit, rotationYLimit);

        cannonTower.localEulerAngles = Vector3.up * rotationY;
    }
    
    // Control the cannonGun rotation
    private void RotationCannonGun(float rotationX)
    {
        rotationX = Mathf.Clamp(rotationX, rotationXMin, rotationXMax);

        cannonGun.localEulerAngles = Vector3.right * -rotationX;
    }
    
    // Control the shoot system of the tank cannon
    private void CannonShoot()
    {
        rechargeTime = Random.Range(1.5f, 4f);
        GameObject projectile = Instantiate(projectilePrefab, cannonGun.position, cannonGun.rotation);
        projectile.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        projectile.gameObject.tag = "EnemyBullet";
        projectile.GetComponent<Rigidbody>().AddForce(cannonGun.forward * impulsionForce, ForceMode.Impulse);
    }
}
