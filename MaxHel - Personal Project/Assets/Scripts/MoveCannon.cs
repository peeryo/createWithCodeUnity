using UnityEngine;

public class MoveCannon : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float rotationLimit = 90;
    private float rotationX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, 0, rotationLimit);

        transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
