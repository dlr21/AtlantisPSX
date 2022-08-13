using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskKey : MonoBehaviour
{
    private Animator desk;

    // Start is called before the first frame update
    void Start()
    {
        desk = gameObject.GetComponent<Animator>();
    }

    public void Open()
    {
        desk.SetTrigger("Open");
        gameObject.GetComponent<Search>().activa(true);
        gameObject.GetComponent<Search>().Fin();
    }
}
