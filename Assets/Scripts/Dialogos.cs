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
    

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        leyendo = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (enZona) {
            if (Input.GetKeyDown("e") && !leyendo)
            {
                Hola();
                pc.Dialog(new Vector3(transform.position.x,0,transform.position.z));
            }
        }

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

    void StartDialogue() {
        index = 0;
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
            StartCoroutine(typeLine());
        }
        else {
            dialog.SetActive(false);
            leyendo = false;
            textComponent.text = string.Empty;
            pc.Dialog(Vector3.zero);
        }
    }

    void Hola() {
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
