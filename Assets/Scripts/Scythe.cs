using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{

    Rigidbody2D rb;
    float spinSpeed = 10f;
    float zAngle = 10f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        rb.MoveRotation(zAngle * spinSpeed * Time.fixedDeltaTime);
        
    }
}
