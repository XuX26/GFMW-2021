using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPusher : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = LevelManager.instance.wallSpeed;
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}