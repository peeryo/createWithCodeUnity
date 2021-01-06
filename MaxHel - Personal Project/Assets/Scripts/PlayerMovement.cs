using UnityEngine;

/* Player movement class script
 * + control how the player can move the tank with keyboard
 * + control the movement speed of the player tank
 */
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 25.0f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed * verticalInput);
        // We turn the vehicle
        transform.Rotate(Vector3.up,Time.deltaTime * rotationSpeed * horizontalInput);
    }
}
