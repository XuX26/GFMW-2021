using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPusher : MonoBehaviour
{
    [Range(0.1f, 10f)] public float speed;
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Collide with wall");
    //         other.transform.position += Vector3.forward * 0.5f;
    //     }
    // }
}