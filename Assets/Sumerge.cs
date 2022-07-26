using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumerge : MonoBehaviour
{

    PlayerController pl;

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            pl.playerState("WaterIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            pl.playerState("WaterOut");
        }
    }
}
