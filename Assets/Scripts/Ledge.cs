using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{

    public Vector3 posHang;
    public Vector3 changeAXIS;
    public Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        endPos = this.transform.position + endPos;
    }

    public Vector3 VectorReal(Transform player) {
        if (changeAXIS.x <= 0.0)
        {
            posHang.x = player.position.x;
        }
        if (changeAXIS.y <= 0.0)
        {
            posHang.y = player.position.y;
        }
        if (changeAXIS.z <= 0.0)
        {
            posHang.z = player.position.z;
        }
        
        return posHang;
    }
}
