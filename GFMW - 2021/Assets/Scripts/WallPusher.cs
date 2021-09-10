using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPusher : MonoBehaviour
{
    private float speed;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Start()
    {
        speed = LevelManager.instance.wallSpeed;
    }

    public void ResetPos()
    {
        transform.position = startPos;
    }

    void Update()
    {
        if (GameManager.instance.state == State.INGAME)
            transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}