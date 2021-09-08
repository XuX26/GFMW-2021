using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [HideInInspector] public SlowMotion slowmo;
    CharacterController charaController;

    [Range(100f, 750f)] public float mouseSensi = 500f;
    [Range(5f, 15f)] public float moveSpeed;

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
        slowmo = GetComponent<SlowMotion>();
        charaController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.instance.state != State.INGAME) return;
        
        GetInput();
        UpdatePlayerRotation();
        Move();
    }

    #region Input
    void GetInput()
    {
        moveDir = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
            slowmo.SwitchSlowmoMode();
    }
    
    #endregion
    
    void UpdatePlayerRotation()
    {
        rotation.x = Input.GetAxisRaw("Mouse X") * mouseSensi * Time.unscaledDeltaTime;
        rotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensi * Time.unscaledDeltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -60f, 60f);

        cam.transform.localRotation = Quaternion.Euler(-rotation.y, 0f, 0f);
        transform.Rotate(Vector3.up * rotation.x);
    }
    
    void Move()
    {
        if (charaController != null)
            charaController.Move(moveDir * moveSpeed * Time.unscaledDeltaTime);
        else
            transform.position += moveDir * moveSpeed * Time.unscaledDeltaTime;
    }

    public void restartPlayerPos()
    {
        
    }
}
