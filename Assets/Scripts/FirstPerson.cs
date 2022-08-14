using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{

    public PlayerController pc;

    public float sensitivity;
    public float rotateHorizontal;
    public float rotateVertical;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (pc.firstPerson) {

            rotateHorizontal += Input.GetAxis("Mouse X") * sensitivity;
            rotateVertical += Input.GetAxis("Mouse Y") * sensitivity;
            var xQuat = Quaternion.AngleAxis(rotateHorizontal, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotateVertical, Vector3.left);

            transform.localRotation = xQuat * yQuat;
        }
    }

    public void Reset()
    {
        rotateHorizontal = 0;
        rotateVertical = 0;
    }
}
