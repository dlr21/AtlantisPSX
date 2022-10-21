using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtrilKey : MonoBehaviour
{
    private Animator atril;

    // Start is called before the first frame update
    void Start()
    {
        atril = gameObject.GetComponent<Animator>();
    }

    public void Open()
    {
        atril.SetTrigger("Open");
        gameObject.GetComponent<Search>().activa(true);
        gameObject.GetComponent<Search>().Fin();
    }
}
