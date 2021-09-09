using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    void Awake()
    {
        Destroy(gameObject, 6f);
    }

    private void Update()
    {
        if(GameManager.instance.state == State.INGAME)
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("laserWall") || other.CompareTag("Player") || other.CompareTag("WallPusher"))
             Destroy(gameObject);
    }
}