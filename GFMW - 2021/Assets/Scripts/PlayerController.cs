using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float mouseSensi = 75f;

    public Camera cam;
    Vector3 moveDir = Vector2.zero;
    Vector2 rotation = Vector2.zero;

    private void Awake()
    {
        if (instance == this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (GameManager.instance.state != State.INGAME) return;
        
        GetInput();
        UpdatePlayerRotation();
    }

    #region Input
    void GetInput()
    {
        moveDir = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    }
    
    void UpdatePlayerRotation()
    {
        rotation.x = Input.GetAxis("Mouse X") * mouseSensi * Time.deltaTime;
        rotation.y += Input.GetAxis("Mouse Y") * mouseSensi * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(-rotation.y, 0f, 0f);
        transform.Rotate(Vector3.up * rotation.x);
    }
    #endregion
    
    void Move()
    {
        transform.position += moveDir;
    }

    private void FixedUpdate()
    {
        Move();
    }
}
