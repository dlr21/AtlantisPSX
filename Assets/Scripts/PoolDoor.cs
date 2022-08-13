using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDoor : MonoBehaviour
{
    
    private GameObject player;
    private Animator door;
    public PlayerController pc;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        door = gameObject.GetComponentInParent<Animator>();
    }


    public void AbrirPuerta() {

        if (gameObject.GetComponent<Search>().enZona)
        {
            door.SetTrigger("Open");
            pc.UsedKey();
            gameObject.GetComponent<Search>().activa(true);
            gameObject.GetComponent<Search>().Fin();
        }
        else {
            pc.ExitMenu();
            gameObject.GetComponentInParent<Dialogos>().EmpezarLectura(false,true);
            Debug.Log("No se puede USAR AQUI");
        }
        
    }

}
