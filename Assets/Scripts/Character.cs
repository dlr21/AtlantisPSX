using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string personaje;//nombre del personaje
    public int[] indexInput;//frase que necesita input
    public string[] input;//input necesitado
    public Sprite icon;//imagen del personaje

    public bool inputIndex(int j) {

        for (int i = 0; i < indexInput.Length; i++) {
            if (indexInput[i] == j) return true;
        }
        return false;
    }

    public string inputWait(int j)//Input.GetKeyDown
    {
            return input[j];
    }

}
