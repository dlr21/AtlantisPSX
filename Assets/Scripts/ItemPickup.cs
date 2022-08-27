using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    // Update is called once per frame
    void PickUp()
    {

        if (item.type != 4)
        {
            if (item.id == 0) {
                Efecto();
            }
            InventoryManager.instance.Add(item);
            Destroy(gameObject);
        }
        else {
            Efecto();
        }
    }

    void Efecto() {
        if (item.id == 0)//Compass
        {
            GameObject.Find("Ellen").GetComponent<Dialogos>().inputExtern();
        }
        else if (item.id == 1)//whitmore portfolio??
        {

        }
        else if (item.id == 27) {//golden heart
            GameObject.Find("Player").GetComponent<PlayerData>().LifeUp();
        }
        else if (item.id == 28) {//clear crystals  
            GameObject.Find("Player").GetComponent<PlayerData>().MoreCrystals();
        }
        else if (item.id == 29) {//letter

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
