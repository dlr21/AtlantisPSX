using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinados : MonoBehaviour
{
    [Header("Puerta a piscina lvl1")]
    public GameObject door;

    [Header("Luces sotano lvl1")]
    public LucesLVL1 luz;

    [Header("Item keyPart lvl1")]
    public DeskKey desk;
    public Item keyPart;



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
        door.GetComponent<Dialogos>().EmpezarLectura(true,true);
        Debug.Log("hace falta USAR una llave");
    }

    public void lvl1_key_desk()
    {
        InventoryManager.instance.Add(keyPart);
        InventoryManager.instance.WhitmoreKey();
        desk.Open();
        Debug.Log("Recogiendo Llave de la mesa");
    }


}
