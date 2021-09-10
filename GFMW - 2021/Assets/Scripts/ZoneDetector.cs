using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneDetector : MonoBehaviour
{
    // public  bool targetDetected;

    public bool debugMode = true;
    // private void Start()
    // {
    //     targetDetected = false;
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         target = other.gameObject;
    //         targetDetected = true;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         target = null;
    //         targetDetected = false;
    //     }
    // }

    private void OnDrawGizmos()
    {
        if(!debugMode) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(10f,2f));
    }
}
