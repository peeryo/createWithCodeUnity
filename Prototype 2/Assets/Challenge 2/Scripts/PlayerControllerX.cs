using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float spawnInterval = 1.0f;

    private bool cooldown = false;
    
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cooldown == false)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                Invoke("ResetCooldown", spawnInterval);
                cooldown = true;
            }
        }
    }
    
    void ResetCooldown(){
        cooldown = false;
    }
}
