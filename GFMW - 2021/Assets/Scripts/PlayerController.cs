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

    private Vector3 startPos;


    private void Awake()
    {
        if (instance == this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        slowmo = GetComponent<SlowMotion>();
        charaController = GetComponent<CharacterController>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (GameManager.instance.state == State.INGAME)
        {
            GetInput();
            UpdatePlayerRotation();
            Move();
        }
    }

    #region Input
    void GetInput()
    {
        rotation.x = Input.GetAxisRaw("Mouse X") * mouseSensi * Time.unscaledDeltaTime;
        rotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensi * Time.unscaledDeltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -60f, 60f);
        moveDir = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
            slowmo.SwitchSlowmoMode();
    }
    
    #endregion

    #region Movement

    void UpdatePlayerRotation()
    {
        cam.transform.localRotation = Quaternion.Euler(-rotation.y, 0f, 0f);
        transform.Rotate(Vector3.up * rotation.x);
    }
    
    void Move()
    {
        charaController.Move(moveDir * moveSpeed * Time.unscaledDeltaTime);
    }
    #endregion

    #region Collision/Death

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") || other.CompareTag("laserWall"))
        {
            Debug.Log("hit by " + other.tag);
            //play sound
            LevelManager.instance.GameOver();
        }
    }

    public void UpdateOnDeath(float lerp)
    {
        transform.Rotate(new Vector3(-0.03f,-0.06f,0));
    }
    
    #endregion
    
    public void StartRun()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        GameManager.instance.ChangeState(State.INGAME);
    }
}
