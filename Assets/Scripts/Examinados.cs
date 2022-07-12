using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinados : MonoBehaviour
{
    [Header("Puerta a piscina lvl1")]
    public PoolDoor pd;


    public void lvl1_luz() {
        Debug.Log("encendemos luces");
    }

    public void lvl1_pasadizo()
    {
        Debug.Log("abrimos trampilla");
    }

    public void lvl1_puerta_piscina()
    {
        pd.AbrirPuerta();
        Debug.Log("hace falta USAR una llave");
    }

}
