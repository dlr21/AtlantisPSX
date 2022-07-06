using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraThird : MonoBehaviour
{
    [Header("Opciones Camara")]
    [Range(0,1)] public float lerpValue;
    public Vector3 offset;
    public float sensibilidad;

    [SerializeField ]private Transform target;
 
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called last once per frame
    void LateUpdate()
    {
        //posicion de la camara
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up)*offset;
        //camara mirando al target
        transform.LookAt(target);
    }
}
