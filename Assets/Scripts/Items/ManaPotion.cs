using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    public float manaToGive;

    GameManagerTwo gameManager;
    Inventory inventory;

    public GameObject itemToAdd;//ekleyeceğimnşiz nesne 
    public int itemAmount;//ekleyeceğimiz nesnenin adedi

    private void Start() 
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("TriggerZone"))
        {
            inventory.CheckSlotsAvailablety(itemToAdd, itemToAdd.name, itemAmount);
            //checkslotsavailablety fonksiyonu bizden öncelikle ekleyeceğimiz nesneyi sonra adını sonra da değerini istiyor
            //collision.GetComponent<PlayerHealth>().currentHealth += healthToGive;
            Destroy(gameObject);
        }
    }
}
