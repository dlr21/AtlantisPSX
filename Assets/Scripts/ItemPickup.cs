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
            InventoryManager.instance.Add(item);
            Destroy(gameObject);
        }
        else {
            Efecto();
        }
    }

    void Efecto() {
        if (item.id == 1) {//whitmore portfolio??

        }
        else if (item.id == 27) {//golden heart

        }
        else if (item.id == 28) {//clear crystals  


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
