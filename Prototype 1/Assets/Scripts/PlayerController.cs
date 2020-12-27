using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horsePower = 20.0f;
    [SerializeField] float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] private TextMeshProUGUI speedometerText;
    [SerializeField] private float speed;
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private float rpm;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] private int wheelsOnGround;
    
    private void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
        
        allWheels = gameObject.GetComponentsInChildren<WheelCollider>().ToList();
    }

    // Update is called once per frame
    // FixedUpdate => Before Update: good for physics and movement
    void FixedUpdate()
    {
        // This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            // Move the vehicle forward
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            // We turn the vehicle
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);
            speedometerText.SetText("Speed: " + speed + " kph");

            rpm = Mathf.Round(allWheels[0].rpm);
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels) 
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}