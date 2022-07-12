using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manos : MonoBehaviour
{
    [Header("Colgando")]
    [SerializeField] private bool colgando = false;

    public Transform orientation;
    public Transform cam;

    public PlayerController pl;


    public LayerMask ledge;

    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        pl.SetColgando(colgando);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enra");
        if (other.gameObject.CompareTag("Ledge")) {
            colgando = true;
            Debug.Log("tag");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("sale");
        if (other.gameObject.CompareTag("Ledge"))
        {
            colgando = false;
            Debug.Log("tag2");
        }

    }
}
