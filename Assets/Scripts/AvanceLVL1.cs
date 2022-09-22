using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

//Esta clase se encarga de desbloquear las cosas en su orden 
public class AvanceLVL1 : MonoBehaviour
{
    [Header("Objetos")]
    public GameObject lucesCaldera;
    public GameObject puertaPiscina;
    public GameObject brujula;

    public GameObject ascensor;
    public PlayableDirector puertasAscensor;


    [Header("Eventos")]
    public UnityEvent[] events;
    [SerializeField]private int evento;


    public void Activa() {
        if(evento < events.Length)events[evento].Invoke();
        
    }

    public void Hola() {
        evento++;
        brujula.SetActive(true);
    }

    public void Brujula() {
        evento++;
        lucesCaldera.SetActive(true);
    }


    public void CombinarLlave()
    {
        evento++;
        puertaPiscina.SetActive(true);
    }

    public void Irse()
    {
        evento++;
        puertasAscensor.Play();
    }

}
