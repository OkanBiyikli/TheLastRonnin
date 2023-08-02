using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    GameManagerTwo gameManager;
    Inventory inventory;
    void Start()
    {
       gameManager = GameManagerTwo.instance;
       inventory = gameManager.GetComponent<Inventory>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {
        inventory.UseInventoryItems(gameObject.name);//useinventoryitemsdaki parametre ile burda verdiğimiz parametre(gameobject.name) uyuşuyorsa button çalışıcak
    }
}
