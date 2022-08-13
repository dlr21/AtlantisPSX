using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Search : MonoBehaviour
{

    [Header("Tiempo de seleccion")]
    [SerializeField] private float indicatorTimer;
    [SerializeField] private float maxindicatorTimer;

    [Header("Imagen")]
    [SerializeField] private Image radialIndicator;


    [Header("Boton para examinar")] 
    [SerializeField] private KeyCode select;

    [Header("Eventos")]
    [SerializeField] private UnityEvent myevent;

    private bool update = false;

    private bool activado = false;

    private bool fin = false;

    [Header("En zona")]
    public bool enZona = false;


    // Update is called once per frame
    void Update()
    {

        if (!activado && enZona)
        {

            GameObject.Find("exclama").gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            if (Input.GetKey(select))
            {
                update = false;
                indicatorTimer += Time.deltaTime;
                radialIndicator.enabled = true;
                radialIndicator.fillAmount = indicatorTimer;

                if (indicatorTimer >= 1)
                {
                    indicatorTimer = 0;
                    radialIndicator.fillAmount = 0;
                    radialIndicator.enabled = false;
                    if(myevent!=null)myevent.Invoke();
                    activado = true;
                    GameObject.Find("exclama").gameObject.transform.localScale = new Vector3(0, 0, 0);
                }

            }
            else
            {

                if (update)
                {
                    indicatorTimer -= Time.deltaTime;
                    radialIndicator.fillAmount = indicatorTimer;

                    if (indicatorTimer <= 0)
                    {


                        indicatorTimer = 0;
                        radialIndicator.fillAmount = 0;
                        radialIndicator.enabled = false;
                        update = false;
                    }
                }

            }

            if (Input.GetKeyUp(select))
            {
                update = true;
            }
        }

        
    }

    public void activa(bool b) {
        activado = b;
    }

    void OnTriggerEnter(Collider collision)
    {
        enZona = true;
    }

    public void Fin() {
        fin = true;
    }

    void OnTriggerExit(Collider collision)
    {
        GameObject.Find("exclama").gameObject.transform.localScale = new Vector3(0, 0, 0);
        enZona = false;
        if (!fin)
        {
            activa(false);
        }
    }
}
