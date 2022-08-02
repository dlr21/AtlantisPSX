using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    PrimaryWeapon pw;
    private Transform curve_point;
    public float rotationSpeed;
    private float time;
    public bool va, vuelve;

    // Start is called before the first frame update
    void Start()
    {
        pw = GameObject.Find("Player").GetComponent<PrimaryWeapon>();
        curve_point = GameObject.Find("CurvePoint").transform;
        time = 0.0f;
        va = true;
        vuelve = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pw.onAir) {
            //giros
            gameObject.transform.Rotate(new Vector3(0f, 30f, 0f)* rotationSpeed * Time.deltaTime);
            //ir y volver
            if (va)
            {
                if (time < 1.0f)
                {
                    gameObject.transform.position = getBQCPoint(time, gameObject.transform.position, curve_point.position, pw.enemyPosition);
                    time += Time.deltaTime;
                }
            }
            else if (vuelve) {
                Debug.Log("vuelve");
                if (time < 1.0f)
                {
                    gameObject.transform.position = getBQCPoint(time, gameObject.transform.position, curve_point.position, pw.transform.position+new Vector3(0,1.5f,0));
                    time += Time.deltaTime;
                }

            }

        }
    }

    //Bezier Quadratic Curve// old pos, curve_point, target
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            va = false;
            vuelve = true;
            time = 0.0f;
            Debug.Log("toca Enemy");
        }
        else if (other.CompareTag("Player") && vuelve) {
            vuelve = false;
            pw.canShoot = true;
            Destroy(gameObject);
            Debug.Log("toca Player");
        }
    }


}
