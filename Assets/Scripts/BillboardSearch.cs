using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSearch : MonoBehaviour
{

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);

        

    }
}
