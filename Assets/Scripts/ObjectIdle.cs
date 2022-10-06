using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIdle : MonoBehaviour
{
    private float yBase;
    private float speedRotation;
    private float speedUpDown;

    void Start()
    {
        yBase = transform.position.y;
        speedRotation = 50;
        speedUpDown = 3;
    }
    
    void Update()
    {
        //Debug.Log(Time.time+" "+Time.deltaTime);
        transform.position = new Vector3(transform.position.x, yBase+Mathf.PingPong(Time.time/speedUpDown, 0.5f), transform.position.z);
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.Self);
    }
}
