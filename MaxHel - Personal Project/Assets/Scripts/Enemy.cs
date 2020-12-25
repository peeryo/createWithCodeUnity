using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Transform towerTr;
    private Transform cannonTr;
    private float cannonAngle = 0;
    private float rechargeTimer = 0f;

    public float forwardSpeed = 10.0f;
    public float turnSpeed = 25.0f;
    public GameObject projectilePrefab;
    public float impulsionForce = 50.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        towerTr = transform.Find("Tower");
        cannonTr = towerTr.Find("Cannon");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 lookRotation = Quaternion.LookRotation(lookDirection).eulerAngles - transform.rotation.eulerAngles;

        if (distance >= 40)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (lookDirection), Time.deltaTime * turnSpeed);
        } else
        {
            float rotationY;
            if (lookRotation.y <= 70 && lookRotation.y >= 0)
            {
                rotationY = Mathf.Clamp(lookRotation.y, 0, 70);
                
                towerTr.localEulerAngles = new Vector3(towerTr.localEulerAngles.x, rotationY, towerTr.localEulerAngles.z);
            } else if (lookRotation.y >= 290 && lookRotation.y <= 360)
            {
                rotationY = Mathf.Clamp(lookRotation.y, 290, 360);
                
                towerTr.localEulerAngles = new Vector3(towerTr.localEulerAngles.x, rotationY, towerTr.localEulerAngles.z);
            } else if (lookRotation.y <= 0 && lookRotation.y >= -70)
            {
                rotationY = Mathf.Clamp(lookRotation.y, -70, 0);
                
                towerTr.localEulerAngles = new Vector3(towerTr.localEulerAngles.x, rotationY, towerTr.localEulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime);
            }
        }

        cannonAngle = (50.0f / 80.0f) * distance;
        cannonAngle = Mathf.Clamp(cannonAngle, 0, 50);
        Debug.Log(cannonAngle);
        
        cannonTr.localEulerAngles = new Vector3(-cannonAngle, cannonTr.localEulerAngles.y, cannonTr.localEulerAngles.z);

        if (rechargeTimer <= 0)
        {
            rechargeTimer = Random.Range(1.5f, 4f);
            GameObject projectile = Instantiate(projectilePrefab, cannonTr.transform.position, cannonTr.transform.rotation * projectilePrefab.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(cannonTr.transform.forward * impulsionForce, ForceMode.Impulse);
        }

        rechargeTimer -= Time.deltaTime;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
