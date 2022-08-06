using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int health;
    [SerializeField] private int lifes;

    [Header("0 Milo 1 Audrey 2 Vini 3 Molier 4 Kida")]
    public int personaje;//0 milo//1 audrey//2 vini//3 molier//4 kida

    [Header("Coleccionables")]
    [SerializeField] private int clearCrystals;

    private void Start()
    {
        health = 50;
        lifes = 5;
        personaje = 0;
        clearCrystals = 0;
    }
    //HAY QUE QUITARLO
    private void Update()
    {

        //Usar arma primaria del personaje
        if (Input.GetKeyDown("l"))
        {
            InventoryManager.instance.AtacarItem();
        }

        if (Input.anyKey)
        {
            Debug.Log(Input.inputString);
        }
    }

    public void MoreCrystals() {
        clearCrystals++;
        if (clearCrystals == 15) {
            LifeUp();
            clearCrystals = 0;
        }
    }

    public void MoreHealth(int h) {
        health = health + h;
        if (health > 100) {
            health = 100;
        }
    }

    public void LessHealth(int h)
    {
        health = health - h;

        if (health < 1) {
            Die();
        }
    }

    public void LifeUp() {
        lifes++;
    }

    public void Die() {
        lifes--;

        if (lifes == 0)
        {
            //no se que pasa cuando llegas a 0 vidas la verdad
        }
        else {
            //LoadSave(); cargar el ultimo savepoint
        }
    }

    public void CambiarPersonaje(int i) {
        personaje = i;
        InventoryManager.instance.ChangeCharacter(i);
        //cambiar modelo personaje
    }

}
