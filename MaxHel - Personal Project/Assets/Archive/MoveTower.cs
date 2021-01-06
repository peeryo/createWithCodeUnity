using UnityEngine;

public class MoveTower : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float rotationLimit = 90;
    
    private float rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, -rotationLimit, rotationLimit);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);
    }
}
