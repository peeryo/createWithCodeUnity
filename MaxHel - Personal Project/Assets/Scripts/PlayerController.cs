using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10.0f;
    public float turnSpeed = 25.0f;
    public GameObject projectilePrefab;
    public float impulsionForce = 50.0f;
    public float rechargeTime = 1.5f;

    private float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");
            
        // Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed * forwardInput);
        // We turn the vehicle
        transform.Rotate(Vector3.up,Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            timer = rechargeTime;
            GameObject projectile = Instantiate(projectilePrefab, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).rotation * projectilePrefab.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).GetChild(0).transform.forward * impulsionForce, ForceMode.Impulse);
        }
        
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("EnemyBullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
