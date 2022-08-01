using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinados : MonoBehaviour
{
    [Header("Puerta a piscina lvl1")]
    public PoolDoor pd;

    [Header("Luces sotano lvl1")]
    public LucesLVL1 luz;


    public void lvl1_luz() {
        luz.switchOn();
        Debug.Log("encendemos luces");
    }

    public void lvl1_pasadizo()
    {
        Debug.Log("abrimos trampilla");
    }

    public void lvl1_puerta_piscina()
    {
        Debug.Log("hace falta USAR una llave");
    }

}
