using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDoor : MonoBehaviour
{
    
    private GameObject player;
    private Animator door;

    [SerializeField]private string llave="key_lvl1";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        door = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirPuerta() {

        if (gameObject.GetComponent<Search>().enZona)
        {
            door.SetTrigger("Open");
        }
        else {
            Debug.Log("No se puede USAR AQUI");
        }
        
    }

}
