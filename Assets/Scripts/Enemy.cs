using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;

    public int range;

    public void LessHealth(int h) {
        health = health - h;
        if (health < 1) {
            Die();
        }
    }

    void Die() {
        //animacion morir
        Destroy(gameObject);
    }

}
