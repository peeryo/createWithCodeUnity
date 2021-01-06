using System;
using UnityEngine;

/* Player cannon class script
 * + control how the player can fire bullets with keyboard
 * + control the bullet launch force of the tank
 * + control how the player can target or aimed with the tank cannon
 */
public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float impulsionForce;
    [SerializeField] private float rechargeTime;
    
    [SerializeField] private float rotationYSpeed;
    [SerializeField] private float rotationYLimit;
    [SerializeField] private float rotationXSpeed;
    [SerializeField] private float rotationXMin;
    [SerializeField] private float rotationXMax;
    
    private float timer = 0.0f;
    private float rotationY = 0f;
    private float rotationX = 0f;
    private Transform cannonTower;
    private Transform cannonGun;

    // Start is called before the first frame update
    void Start()
    {
        cannonTower = transform.GetChild(0);
        cannonGun = cannonTower.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        RotateCannonTower();
        RotationCannonGun();
    }

    // FixedUpdate is called once per frame before Update
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            CannonShoot();
        }
        
        timer -= Time.deltaTime;
    }

    // Control the cannon tower rotation with mouse horizontal movement
    private void RotateCannonTower()
    {
        rotationY += Input.GetAxis("Mouse X") * rotationYSpeed;
        rotationY = Mathf.Clamp(rotationY, -rotationYLimit, rotationYLimit);

        cannonTower.localEulerAngles = Vector3.up * rotationY;
    }

    // Control the cannonGun rotation with mouse vertical movement
    private void RotationCannonGun()
    {
        rotationX += Input.GetAxis("Mouse Y") * rotationXSpeed;
        rotationX = Mathf.Clamp(rotationX, rotationXMin, rotationXMax);

        cannonGun.localEulerAngles = Vector3.right * -rotationX;
    }

    // Control the shoot system of the tank cannon
    private void CannonShoot()
    {
        timer = rechargeTime;
        GameObject projectile = Instantiate(projectilePrefab, cannonGun.position, cannonGun.rotation);
        projectile.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        projectile.gameObject.tag = "PlayerBullet";
        projectile.GetComponent<Rigidbody>().AddForce(cannonGun.forward * impulsionForce, ForceMode.Impulse);
    }
}
