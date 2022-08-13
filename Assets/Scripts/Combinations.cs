using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinations : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    private bool pos1Full;
    private bool pos2Full;

    [SerializeField] private GameObject comb1;
    [SerializeField] private GameObject comb2;

    [Header("Items auxiliares")]
    public Item a;
    public Item b;

    [Header("Items creados")]
    public Item oldKey;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        pos1Full = false;
        pos2Full = false;
    }


    public void CalculatePositions() {

        Vector3 pos = cam.transform.position + cam.transform.forward;
        pos2 = pos + cam.transform.right/4 + cam.transform.up / 5;
        pos1 = pos - cam.transform.right/4 + cam.transform.up / 5;

        //Instantiate(a, pos1, Camera.main.transform.rotation);
        //Instantiate(b, pos2, Camera.main.transform.rotation);

    }

    public void Combines(Item g) {
        if (!pos1Full)
        {
            comb1=Instantiate(g.icon, pos1, Camera.main.transform.rotation);
            a = g;
            pos1Full = true;
        }
        else if (/*!pos2Full &&*/ a.id != g.id)
        {
            b = g;
            comb2 = Instantiate(g.icon, pos2, Camera.main.transform.rotation);
            if (IntentaCombinar())
            {   
                Debug.Log("AnimacionCombinar");
            }
            else
            {
                b = null;
                Debug.Log("No puedes combinar esto");
            }

        }
        else {
            Debug.Log("Error burro");
        }

    }

    bool IntentaCombinar() {
        Debug.Log(a.id + " " + b.id);
        //HandleKey y EndKey
        if ((a.id == 2 && b.id == 4) || (b.id == 2 && a.id == 4)) {

            CombinacionEnInventario(oldKey);

            return true;
        }
       
        return false;
    }

    void CombinacionEnInventario(Item i) {
        InventoryManager.instance.Keys.Remove(a);
        InventoryManager.instance.Keys.Remove(b);
        LimpiarCombinacion();
        InventoryManager.instance.Hide();
        InventoryManager.instance.Add(i);
        InventoryManager.instance.Show();
    }

    void LimpiarCombinacion() {
        Destroy(comb1);
        Destroy(comb2);
        pos1Full = false;
        a = null;
        b = null;
    }

}
