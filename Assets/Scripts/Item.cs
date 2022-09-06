using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/New Item")]

public class Item : ScriptableObject
{

    public int id;
    public string itemName;
    [Header("0 Basico 1 Consumible 2 Crystal 3 Key")]
    public int type; //0basic,1 consumible
    public int value;
    public Quaternion rotationPlus;
    public GameObject icon;

    public string description;

    public void Use() {
        if (type == 0) {
            UseBasic();
        } else if (type == 1) {
            UseConsumible();
        } else if (type == 2) {
            UseCrystal();
        } else if (type == 3) {
            UseKey();
        }
    }

    void UseBasic()
    {
        if (id == 0) //Compass
        {
           
        }
        else if (id == 1) //WhitmorePortfolio
        {
            GameObject.Find("Ellen").GetComponent<Dialogos>().inputExtern();//cuenta todos los personajes
        }
        else if (id == 6) //Boomerang 
        {
            GameObject.Find("Player").GetComponent<PrimaryWeapon>().Boomerang();
        }
        else if (id == 7) //Grenades
        {
            GameObject.Find("Player").GetComponent<PrimaryWeapon>().Grenades();
        }
        else if (id == 10) //HandfulofRocks
        {
            GameObject.Find("Player").GetComponent<PrimaryWeapon>().HandfulofRocks();
        }
        else if (id == 15) //BoltStaff
        {
            GameObject.Find("Player").GetComponent<PrimaryWeapon>().BoltStaff();
        } else if (id == 30) //FlareGun
        {
            GameObject.Find("Player").GetComponent<PrimaryWeapon>().FlareGun();
        }
        else{
            NoOption();
        }


    }

    void UseConsumible()
    {
        if (id==100) {

        }else {
            NoOption();
        }
    }

    void UseCrystal()
    {
        if (id == 100)
        {

        }
        else
        {
            NoOption();
        }
    }

    void UseKey()
    {
        if (id == 100) {

        }
        else if (id == 5) //oldkey//pool_lvl1
        {
            GameObject.Find("ExaminarPuerta").GetComponent<PoolDoor>().AbrirPuerta();
        }else {
            NoOption();
        }
    }

    void NoOption() {
        Debug.Log("esto objeto no tiene esta opcion");
    }

    public void Combine()
    {
        if (type == 0)
        {
            CombineBasic();
        }
        else if (type == 1)
        {
            CombineConsumible();
        }
        else if (type == 2)
        {
            CombineCrystal();
        }
        else if (type == 3)
        {
            CombineKey();
        }
    }

    void CombineBasic()
    {
        if (id == 0) //Compass
        {

        }
        else if (id == 1) //WhitmorePortfolio?
        {

        }
        else if (id == 6) //Boomerang 
        {

        }
        else if (id == 7) //Grenades
        {

        }
        else if (id == 10) //HandfulofRocks
        {

        }
        else if (id == 15) //BoltStaff
        {

        }
        else if (id == 30) //FlareGun
        {

        }else
        {
            NoOption();
        }


    }
    void CombineConsumible() {
        if (id == 100)
        {

        }
        else
        {
            NoOption();
        }
    }
    void CombineCrystal() {
        if (id == 100)
        {

        }
        else
        {
            NoOption();
        }
    }
    void CombineKey() {
        if (id == 100)
        {

        }
        else if (id == 2) {//EndKey
            GameObject.Find("InventoryManager").GetComponent<Combinations>().Combines(this);
        }
        else if (id == 4)//HandleKey
        {
            GameObject.Find("InventoryManager").GetComponent<Combinations>().Combines(this);
        }
        else{
            NoOption();
        }
    }
}
