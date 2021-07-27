using UnityEngine;

public class CarController : MonoBehaviour
{
    private Transform _carTransform;
    public int carSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _carTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _carTransform.transform.Translate(Vector3.forward * (Time.deltaTime * carSpeed));
    }
}
