using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public int both;

    public static InventoryManager instance;
    public bool showing,selected;
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

    public int optionSelected;
    public GameObject nombre;
    public GameObject usar;
    public GameObject combinar;
    public GameObject seleccionar;

    private Color colText1;
    private Color colText2;

    private void Awake()
    {
        instance = this;
        optionV = 0;
        optionH = 0;
        showing = false;
        optionSelected = 0;
        colText1 = new Color(255, 255, 255, 255);
        colText1.a=1f;

        colText2 = new Color(255, 255, 255, 255);
        colText2.a = 0.5f;
    }

    public void Update()
    {
        if (showing) {

            Inputs();

            nombre.GetComponent<TextMeshProUGUI>().text = nombreItem();
           
        }
    }

   private string nombreItem() {

        if (optionV == 0)
        {
            if(optionH < Basics.Count && Basics[optionH]!=null) return Basics[optionH].name;
        }
        else if (optionV == 1) {
            if (optionH < Consumibles.Count && Consumibles[optionH] != null) return Consumibles[optionH].name;
        }
        else if (optionV == 2)
        {
            if (optionH < Crystals.Count && Crystals[optionH] != null) return Crystals[optionH].name;
        }
        else if (optionV == 3)
        {
            if (optionH < Keys.Count && Keys[optionH] != null) return Keys[optionH].name;
        }



        return "No item";
    }

    public void Inputs() {

        if (!selected)
        {

            if (Input.GetKeyDown("w"))
            {
                if (optionV < 4 && optionV>0)
                {
                    optionV--;
                    optionH = 0;
                    ChangeOptionVertical(true);

                }
            }

            if (Input.GetKeyDown("s"))
            {
                if (optionV > -1 && optionV<3)
                {
                    optionV++;
                    optionH = 0;
                    ChangeOptionVertical(false);

                }
            }

            if (Input.GetKeyDown("a"))
            {
                optionH--;
                if (controlOptionH()) {
                    
                    ChangeOptionHorizontal(false);
                }
                else
                {
                    optionH++;
                }

            }

            if (Input.GetKeyDown("d"))
            {
                optionH++;
                if (controlOptionH())
                {

                    ChangeOptionHorizontal(true);
                }
                else {
                    optionH--;
                }
            }

            if (Input.GetKeyDown("return")) {


                if (itemExist())
                {
                    selected = true;
                    usar.GetComponent<TextMeshProUGUI>().color = colText1;
                    seleccionar.GetComponent<TextMeshProUGUI>().color = colText2;
                }

            }
        }
        else {
            
            if (Input.GetKeyDown("s"))
            {
                //cambia a combinar
                if (optionSelected==0)
                {
                    optionSelected++;
                    combinar.GetComponent<TextMeshProUGUI>().color = colText1;
                    usar.GetComponent<TextMeshProUGUI>().color = colText2;
                }
            }

            if (Input.GetKeyDown("w"))
            {
                //cambia a usar
                if (optionSelected==1)
                {
                    optionSelected--;
                    combinar.GetComponent<TextMeshProUGUI>().color = colText2;
                    usar.GetComponent<TextMeshProUGUI>().color = colText1;
                }
            }

            if (Input.GetKeyDown("return")) {
                if (optionSelected == 0)
                {
                    UseItem();
                    Debug.Log("usar");
                }
                else if (optionSelected == 1) {
                    CombineItem();
                    Debug.Log("Combinar");
                }
            }

            if (Input.GetKeyDown("space"))
            {
                selected = false;
                combinar.GetComponent<TextMeshProUGUI>().color = colText2;
                usar.GetComponent<TextMeshProUGUI>().color = colText2;
                seleccionar.GetComponent<TextMeshProUGUI>().color = colText1;
            }

        }



    }

    bool itemExist()
    {
        if (optionV == 0)
        {
            if (MenuBasic[optionH]==null) return false;
        }
        else if (optionV == 1)
        {
            if (MenuConsumibles[optionH] == null) return false;
        }
        else if (optionV == 2)
        {
            if (MenuCrystals[optionH] == null) return false;
        }
        else if (optionV == 3)
        {
            if (MenuKeys[optionH] == null) return false;
        }
        return true;

    }

    public void AtacarItem() {
        if (0 < Basics.Count && Basics[0] != null) Basics[0].Use();
    }

    bool controlOptionH() {
        if (optionV == 0)
        {
            if (optionH  > Basics.Count-1 || optionH < 0) return false ;
        }
        else if (optionV == 1)
        {
            if (optionH  > Consumibles.Count-1 || optionH  < 0) return false;
        }
        else if (optionV == 2)
        {
            if (optionH > Crystals.Count-1 || optionH  < 0) return false;
        }
        else if (optionV == 3)
        {
            if (optionH > Keys.Count-1 || optionH  < 0) return false;
        }
        return true;
    }

    //mover objetos en la misma lista de menu
    public void ChangeOptionHorizontal(bool op)
    {
        Vector3 aux= Camera.main.transform.right;

        if (op)
        {
            aux = -aux/3;
        }
        else
        {
            aux = aux/3;
        }

        switch (optionV) {
            case 0:
                //if (optionH < 1 || optionH > MenuBasic.Count ) { break; }
                for (int i = 0; i < MenuBasic.Count; i++)
                {
                    MenuBasic[i].transform.position = MenuBasic[i].transform.position + aux;
                }

                break;
            case 1:
                //if (optionH < 1 || optionH > MenuConsumibles.Count ) { break; }
                for (int i = 0; i < MenuConsumibles.Count; i++)
                {
                    MenuConsumibles[i].transform.position = MenuConsumibles[i].transform.position + aux;
                }

                break;
            case 2:
                //if (optionH < 1 || optionH > MenuCrystals.Count ) { break; }
                for (int i = 0; i < MenuCrystals.Count; i++)
                {
                    MenuCrystals[i].transform.position = MenuCrystals[i].transform.position + aux;
                }

                break;
            case 3:
                //if (optionH < 1 || optionH > MenuKeys.Count ) { break; } 
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
        reposicionarMenuV();

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

        gameObject.GetComponent<Combinations>().CalculatePositions();

        inventPanel.SetActive(true);
        showing = true;
        selected = false;
        optionV = 0;
        optionH = 0;

        combinar.GetComponent<TextMeshProUGUI>().color = colText2;
        usar.GetComponent<TextMeshProUGUI>().color = colText2;
        seleccionar.GetComponent<TextMeshProUGUI>().color = colText1;

        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;
        Vector3 posaux = pos;

        for (int i = 0; i < Basics.Count; i++)
        {
            MenuBasic.Add(Instantiate(Basics[i].icon, posaux, Camera.main.transform.rotation));
            posaux=posaux + Camera.main.transform.right/3;
        }
        pos.y = pos.y + 1;
        posaux = pos;
        for (int i = 0; i < Consumibles.Count; i++)
        {
            MenuConsumibles.Add(Instantiate(Consumibles[i].icon, posaux, Camera.main.transform.rotation));
            posaux = posaux + Camera.main.transform.right / 3;
        }
        pos.y = pos.y + 1;
        posaux = pos;
        for (int i = 0; i < Crystals.Count; i++)
        {
            MenuCrystals.Add(Instantiate(Crystals[i].icon, posaux, Camera.main.transform.rotation ));
            posaux = posaux + Camera.main.transform.right / 3;
        }
        pos.y = pos.y + 1;
        posaux = pos;
        for (int i = 0; i < Keys.Count; i++)
        {
            MenuKeys.Add(Instantiate(Keys[i].icon, posaux, Camera.main.transform.rotation));
            posaux = posaux + Camera.main.transform.right / 3;

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

    public void CombineItem()
    {

        if (optionV == 0)
        {
            Basics[optionH].Combine();
        }
        else if (optionV == 1)
        {
            Consumibles[optionH].Combine();
        }
        else if (optionV == 2)
        {
            Crystals[optionH].Combine();
        }
        else if (optionV == 3)
        {
            Keys[optionH].Combine();
        }

    }

    public void ChangeCharacter(int i)
    {
        if (i == 0)
        {
            Basics[0] = boomerang;
        }
        else if (i == 1)
        {
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

    void reposicionarMenuV() {
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward;
        Vector3 posaux = pos;

        if (optionV == 0)
        {
            for (int i = 0; i < MenuBasic.Count; i++)
            {
                MenuBasic[i].transform.position = posaux;
                posaux = posaux + Camera.main.transform.right / 3;
            }
        }
        else if (optionV == 1)
        {
            for (int i = 0; i < MenuConsumibles.Count; i++)
            {
                MenuConsumibles[i].transform.position = posaux;
                posaux = posaux + Camera.main.transform.right / 3;
            }
        }
        else if (optionV == 2)
        {
            for (int i = 0; i < MenuCrystals.Count; i++)
            {
                MenuCrystals[i].transform.position = posaux;
                posaux = posaux + Camera.main.transform.right / 3;
            }
        }
        else if (optionV == 3)
        {
            for (int i = 0; i < MenuKeys.Count; i++)
            {
                MenuKeys[i].transform.position = posaux;
                posaux = posaux + Camera.main.transform.right / 3;
            }
        }
    }

    public void WhitmoreKey() {
        both++;
        if (both == 2) {
            GameObject.Find("Ellen").GetComponent<Dialogos>().inputExtern();
        }
    }

}
