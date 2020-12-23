using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10.0f;
    public float turnSpeed = 25.0f;

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
    }
}
