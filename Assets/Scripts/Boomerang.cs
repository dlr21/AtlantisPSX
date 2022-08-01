using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    PrimaryWeapon pw;
    // Start is called before the first frame update
    void Start()
    {
        pw = GameObject.Find("Player").GetComponent<PrimaryWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.onAir) {

            //gameObject.transform.position

        }
    }


}
