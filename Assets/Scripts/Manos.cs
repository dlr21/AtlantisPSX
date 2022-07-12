using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manos : MonoBehaviour
{

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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge")) {
            pl.SetColgando(true,other);
            pl.PlayerPosition(other.gameObject.GetComponent<Ledge>().VectorReal(pl.gameObject.transform));
            pl.ledge = other.gameObject.GetComponent<Ledge>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            pl.SetColgando(false, other);
        }

    }
}
