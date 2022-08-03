using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public bool showing;
    public GameObject inventPanel;
    public Item boomerang, granades, bolt, flare, rocks;

    public List<Item> Basics = new List<Item>();
    public List<Item> Consumibles = new List<Item>();
    public List<Item> Crystals = new List<Item>();
    public List<Item> Keys = new List<Item>();

    public List<GameObject> MenuBasic = new List<GameObject>();
    public List<GameObject> MenuConsumibles = new List<GameObject>();
    public List<GameObject> MenuCrystals = new List<GameObject>();
    public List<GameObject> MenuKeys = new List<GameObject>();

    public int optionV;
    public int optionH;



    private void Awake()
    {
        instance = this;
        optionV = 0;
        optionH = 0;
        showing = false;
    }

    public void Update()
    {
        if (showing) {

            Inputs();
           
        }
    }

    public void Inputs() {

        if (Input.GetKeyDown("w"))
        {
            if (optionV < 3)
            {
                optionV++;
                optionH = 0;
                ChangeOptionVertical(true);
                Debug.Log("w");
            }
        }

        if (Input.GetKeyDown("s"))
        {
            if (optionV > 0) {
                optionV--;
                optionH = 0;
                ChangeOptionVertical(false);
                Debug.Log("s");
            }
        }

        if (Input.GetKeyDown("a"))
        {
            optionH--;
            ChangeOptionHorizontal(true);
        }

        if (Input.GetKeyDown("d"))
        {
            optionH++;
            ChangeOptionHorizontal(false);
        }

        if (Input.GetKeyDown("l"))
        {
            Basics[0].Use();
        }

    }

    //mover objetos en la misma lista de menu
    public void ChangeOptionHorizontal(bool op)
    {
        Vector3 aux= Camera.main.transform.right;

        if (op)
        {
            aux = aux/4;
        }
        else
        {
            aux = -aux/4;
        }

        Debug.Log("vert "+optionV);

        switch (optionV) {
            case 3:
                if (optionH < 1 || optionH > MenuBasic.Count - 1) { break; }
                for (int i = 0; i < MenuBasic.Count; i++)
                {
                    MenuBasic[i].transform.position = MenuBasic[i].transform.position + aux;
                }

                break;
            case 2:
                if (optionH < 1 || optionH > MenuConsumibles.Count - 1) { break; }
                for (int i = 0; i < MenuConsumibles.Count; i++)
                {
                    MenuConsumibles[i].transform.position = MenuConsumibles[i].transform.position + aux;
                }

                break;
            case 1:
                if (optionH < 1 || optionH > MenuCrystals.Count - 1) { break; }
                for (int i = 0; i < MenuCrystals.Count; i++)
                {
                    MenuCrystals[i].transform.position = MenuCrystals[i].transform.position + aux;
                }

                break;
            case 0:
                if (optionH < 1 || optionH > MenuKeys.Count -1) { break; } 
                for (int i = 0; i < MenuKeys.Count; i++)
                {
                    MenuKeys[i].transform.position = MenuKeys[i].transform.position + aux;
                }

                break;

        }
            
    }

    //mover objetos al cambiar de lista de menu
    public void ChangeOptionVertical(bool op) {

        Vector3 aux;
        if (op)
        {
             aux = new Vector3(0, 1, 0);
        }
        else {
             aux = new Vector3(0, -1, 0);
        }

        for (int i = 0; i < MenuBasic.Count; i++)
        {
            MenuBasic[i].transform.position = MenuBasic[i].transform.position + aux;
        }
        
        for (int i = 0; i < MenuConsumibles.Count; i++)
        {
            MenuConsumibles[i].transform.position = MenuConsumibles[i].transform.position + aux;
        }

        for (int i = 0; i < MenuCrystals.Count; i++)
        {
            MenuCrystals[i].transform.position = MenuCrystals[i].transform.position + aux;
        }

        for (int i = 0; i < MenuKeys.Count; i++)
        {
            MenuKeys[i].transform.position = MenuKeys[i].transform.position + aux;
        }

    }

    public void Add(Item it)
    {

        if (it.type == 0) {
            Basics.Add(it);
        }
        if (it.type == 1)
        {
            Consumibles.Add(it);
        }
        if (it.type == 2)
        {
            Crystals.Add(it);
        }
        if (it.type == 3)
        {
            Keys.Add(it);
        }
        
    }

    public void Remove(Item it)
    {

        if (it.type == 0)
        {
            Basics.Remove(it);
        }
        if (it.type == 1)
        {
            Consumibles.Remove(it);
        }
        if (it.type == 2)
        {
            Crystals.Remove(it);
        }
        if (it.type == 3)
        {
            Keys.Remove(it);
        }

    }

    
    public void Show() {
        inventPanel.SetActive(true);
        showing = true;
        optionV = 3;
        optionH = 0;
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;

        for (int i = 0; i < Basics.Count; i++)
        {
            MenuBasic.Add(Instantiate(Basics[i].icon, pos, Camera.main.transform.rotation));
        }
        pos.y = pos.y + 1;
        for (int i = 0; i < Consumibles.Count; i++)
        {
            MenuConsumibles.Add(Instantiate(Consumibles[i].icon, pos, Camera.main.transform.rotation));
        }
        pos.y = pos.y + 1;
        for (int i = 0; i < Crystals.Count; i++)
        {
            MenuCrystals.Add(Instantiate(Crystals[i].icon, pos, Camera.main.transform.rotation));
        }
        pos.y = pos.y + 1;
        for (int i = 0; i < Keys.Count; i++)
        {
            MenuKeys.Add(Instantiate(Keys[i].icon, pos, Camera.main.transform.rotation));
        }
        

    }

    public void Hide() {
        inventPanel.SetActive(false);
        showing = false;
        for (int i = MenuBasic.Count - 1; i >= 0; i--) {
            GameObject go2 = MenuBasic[i];
            MenuBasic.Remove(go2);
            Destroy(go2);
        }

        for (int i = MenuConsumibles.Count - 1; i >= 0; i--)
        {
            GameObject go2 = MenuConsumibles[i];
            MenuConsumibles.Remove(go2);
            Destroy(go2);
        }

        for (int i = MenuCrystals.Count - 1; i >= 0; i--)
        {
            GameObject go2 = MenuCrystals[i];
            MenuCrystals.Remove(go2);
            Destroy(go2);
        }

        for (int i = MenuKeys.Count - 1; i >= 0; i--)
        {
            GameObject go2 = MenuKeys[i];
            MenuKeys.Remove(go2);
            Destroy(go2);
        }


    }

    public void UseItem()
    {

        if (optionV == 0)
        {
            Basics[optionH].Use();
        }
        else if (optionV == 1)
        {
            Consumibles[optionH].Use();
        }
        else if (optionV == 2)
        {
            Crystals[optionH].Use();
        }
        else if (optionV == 3)
        {
            Keys[optionH].Use();
        }

    }

    public void ChangeCharacter(int i) {
        if (i == 0)
        {
            Basics[0] = boomerang ;
        }
        else if (i == 1) {
            Basics[0] = flare;
        }
        else if (i == 2)
        {
            Basics[0] = granades;
        }
        else if (i == 3)
        {
            Basics[0] = rocks;
        }
        else if (i == 4)
        {
            Basics[0] = bolt;
        }
    }
}
