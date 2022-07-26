using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    private string surface;

    private void OnTriggerEnter(Collider other)
    {
        surface=other.tag;
    }

}
