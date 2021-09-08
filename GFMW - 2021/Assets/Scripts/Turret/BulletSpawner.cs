using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private GameObject target;
    private bool targetLocked;

    public GameObject turret;
    public GameObject bullet;
    public float fireCooldown;
    private bool shotReady;

    private void Start()
    {
        shotReady = true;
    }

    void Update()
    {
        if(targetLocked)
        {
            if (shotReady)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Transform _bullet = Instantiate(bullet.transform, transform.position, Quaternion.identity);
        _bullet.transform.rotation = turret.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            targetLocked = true;
        }
    }
}
