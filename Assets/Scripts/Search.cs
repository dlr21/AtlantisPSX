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
                    myevent.Invoke();
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


                        indicatorTimer = maxindicatorTimer;
                        radialIndicator.fillAmount = maxindicatorTimer;
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


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("entro");
        enZona = true;
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("salgo");
        enZona = false;
    }
}
