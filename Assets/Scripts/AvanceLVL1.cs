using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Esta clase se encarga de desbloquear las cosas en su orden 
public class AvanceLVL1 : MonoBehaviour
{
    [Header("Objetos")]
    public GameObject lucesCaldera;
    public GameObject puertaPiscina;
    public GameObject brujula;
    //public GameObject ascensor;


    [Header("Eventos")]
    public UnityEvent[] events;
    [SerializeField]private int i;


    public void Activa() {
        if(i<events.Length)events[i].Invoke();
    }

    public void Hola() {
        i++;
        brujula.SetActive(true);
    }

    public void Brujula() {
        i++;
        lucesCaldera.SetActive(true);
    }


    public void CombinarLlave()
    {
        i++;
        puertaPiscina.SetActive(true);
    }

    public void Irse()
    {
        i++;
        //ascensor.SetActive(true);
    }

}
