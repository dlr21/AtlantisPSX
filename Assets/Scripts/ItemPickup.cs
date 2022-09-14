using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    
    void PickUp()
    {

        if (item.type != 4)
        {
            if (item.id == 0 || item.id==1 || item.id==6)
            {
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
        if (item.id == 0 || item.id == 6)//Compass o boomerang
        {
            GameObject.Find("Ellen").GetComponent<Dialogos>().inputExtern();
        }
        else if (item.id == 1)//whitmore portfolio
        {
            InventoryManager.instance.WhitmoreKey();
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
