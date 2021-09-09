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
    public SoundEffect sfxShot;
    public float delay;

    private bool istriggered;
    [SerializeField] Transform maxRange;

    private void Awake()
    {
        sfxShot.InitSource(gameObject);
    }

    private void Update()
    {
        if (IsInRange())
        {
            if (ShouldShot())
                ShotABullet();
        }
        sfxShot.UpdatePitchToTimeScale();
    }

    bool IsInRange()
    {
        if (maxRange)
        {
            float playerPosZ = PlayerController.instance.transform.position.z;
            return (playerPosZ > maxRange.position.z && playerPosZ < transform.position.z + 1);
        }
        Debug.LogWarning("null ref on maxRange so the turret always shot");
        return true;
    }

    private bool ShouldShot()
    {
        if (!istriggered)
        {
            istriggered = true;
            lastShot = Time.time - fireRate + delay;
        }
        return (Time.time - lastShot > fireRate);
    }

    void ShotABullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab,transform).GetComponent<Bullet>();
        newBullet.bulletSpeed = bulletSpeed;
        lastShot = Time.time;
        Debug.Log("SHOT!");
        AudioManager.instance.PlaySfx(sfxShot);
    }
}
