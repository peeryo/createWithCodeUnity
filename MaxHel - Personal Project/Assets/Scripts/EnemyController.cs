using UnityEngine;

/* Enemy parameters supervisor script
 * + control enemy's gameobjects conditions of life and death
 */
public class EnemyController : MonoBehaviour
{
    public int lives;
    
    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
