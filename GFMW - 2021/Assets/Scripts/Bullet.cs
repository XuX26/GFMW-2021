using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float bulletSpeed = 5f;
    public SoundEffect sfx;

    void Awake()
    {
        sfx.InitSource(gameObject);
        sfx.source.spatialBlend = 1;
        Destroy(gameObject, 6f);
    }

    private void Update()
    {
        if (GameManager.instance.state == State.INGAME)
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            transform.Rotate(Vector3.back * bulletSpeed/10);
        }
        else
            Destroy(gameObject);
     
        sfx.UpdatePitchToTimeScale();
    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("laserWall") || other.CompareTag("Player") || other.CompareTag("WallPusher"))
             Destroy(gameObject);
    }
}