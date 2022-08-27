using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogos : MonoBehaviour
{

    public GameObject dialog;
    public TextMeshProUGUI textComponent;
    public PlayerController pc;
    public string[] lines;
    public float speed;
    public bool leyendo;
    public bool enZona;

    [Header("Personaje y espera")]
    public Character ch;
    public bool input;
    public int indexInput;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        leyendo = false;
        input = false;
        indexInput = 0;
        index=0;
    }

    // Update is called once per frame
    void Update()
    {

        EmpezarLectura(true,false);

        //clic al leer 
        if (Input.GetMouseButtonDown(0) && leyendo) {

            if (textComponent.text == lines[index]) {
                
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void EmpezarLectura(bool a,bool activ) {
        if (enZona==a)
        {
            if ((Input.GetKeyDown("e") && !leyendo) || activ)
            {
                Hola();
                //mirar a quien hablas
                pc.Dialog(new Vector3(transform.position.x, 0, transform.position.z));
            }
        }
    }


    public void inputExtern() {
        indexInput++;
        index++;
    }

    void StartDialogue() {
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine() {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            if (ch.inputIndex(index))
            {
                index--;
                StopDialog();
            }
            else {
                StartCoroutine(typeLine());
            }
            
        }
        else {
            StopDialog();
        }
    }

    void StopDialog() {
        dialog.SetActive(false);
        leyendo = false;
        textComponent.text = string.Empty;
        pc.Dialog(Vector3.zero);
    }

    void Hola() {
        textComponent.text = string.Empty;
        dialog.SetActive(true);
        leyendo = true;
        StartDialogue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            enZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = false;
        }
    }
}
