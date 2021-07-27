using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSmooth : MonoBehaviour
{
    private float speed = 20;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    public GameObject wideViewCamera;
    public GameObject shortViewCamera;

    public int inputID;

    private bool shortViewCameraActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + inputID); 
        forwardInput = Input.GetAxis("Vertical" + inputID);

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        SwitchCamera();
    }

    private void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!shortViewCameraActive)
            {
                shortViewCamera.SetActive(true);
                wideViewCamera.SetActive(false);
                shortViewCameraActive = true;
            }
            else
            {
                shortViewCamera.SetActive(false);
                wideViewCamera.SetActive(true);
                shortViewCameraActive = false;
            }
        }
    }
}
