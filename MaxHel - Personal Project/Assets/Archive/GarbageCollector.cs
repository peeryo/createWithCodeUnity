﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("EnemyBullet") || collider.CompareTag("PlayerBullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
