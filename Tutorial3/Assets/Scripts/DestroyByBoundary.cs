﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Shield")
        {
            Destroy(other.gameObject);
        }
        if (other.tag != "Triple")
        {
            Destroy(other.gameObject);
        }
        if (other.tag != "Speed")
        {
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
