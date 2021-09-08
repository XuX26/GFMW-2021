using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Bullet : MonoBehaviour
{
    [Range(1f, 10f)] public float bulletSpeed = 5f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 6f);
    }
}