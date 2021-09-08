using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}
