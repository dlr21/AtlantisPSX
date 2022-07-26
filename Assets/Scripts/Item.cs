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
    public GameObject icon;

    public string description;

}
