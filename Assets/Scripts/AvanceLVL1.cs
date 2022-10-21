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
    public GameObject atril;

    public PlayableDirector puertasAscensor;


    [Header("Eventos")]
    public UnityEvent[] events;
    [SerializeField]private int evento;

    [Header("Controle de repeticion")]
    public bool holaControl;
    public bool brujulaControl;
    public bool combinarControl;
    public bool irseControl;


    public void Activa() {
        if(evento < events.Length)events[evento].Invoke();
    }

    public void Hola()
    {
        if (!holaControl) { 
            evento++;
            brujula.SetActive(true);
            holaControl = true;
        }
    }

    public void Brujula() {
        if (!brujulaControl)
        {
            evento++;
            lucesCaldera.SetActive(true);
            brujulaControl = true;
        }
    }


    public void CombinarLlave()
    {
        if (!combinarControl)
        {
            evento++;
            puertaPiscina.SetActive(true);
            combinarControl = true;
        }
    }

    public void Irse()
    {
        if (!irseControl)
        {
            evento++;
            puertasAscensor.Play();
            atril.SetActive(true);
            irseControl = true;
        }
    }

}
