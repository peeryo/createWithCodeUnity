﻿using UnityEngine;

/* Player parameters supervisor script
 * + control player's conditions of life and death
 */
public class PlayerController : MonoBehaviour
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
