using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesLVL1 : MonoBehaviour
{
    private Animator luz;
    public GameObject search;
    
    // Start is called before the first frame update
    void Start()
    {
        luz = gameObject.GetComponent<Animator>();
    }

    public void switchOn()
    {
        luz.SetTrigger("switchOn");
        search.GetComponent<Search>().activa(true);
        search.GetComponent<Search>().Fin();
    }
}
