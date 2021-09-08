using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Range(1f, 10f)]
    public float bulletSpeed = 5f;
    [Range(0.25f, 5f)]
    public float fireRate;
    private float lastShot;

    [Space]
    public GameObject bulletPrefab;
    public BoxCollider trigger;
    public float delay;
    bool isTriggered = true;

    private float debugTimer;
    

    private void Start()
    {
        ActiveSpawner();
    }

    void ActiveSpawner()
    {
        isTriggered = true;
        lastShot = Time.time - fireRate + delay;
    }

    private void Update()
    {
        debugTimer = Time.time;
        if (ShouldShot())
            ShotABullet();
    }

    private bool ShouldShot()
    {
        return (isTriggered && Time.time - lastShot > fireRate);
    }

    void ShotABullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab,transform).GetComponent<Bullet>();
        newBullet.bulletSpeed = bulletSpeed;
        lastShot = Time.time;
        Debug.Log("SHOT!");
    }
}
