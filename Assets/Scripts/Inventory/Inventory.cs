using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;//slotların tutucak
    //public GameObject[] backpack;//birden fazla çantamız olursa diye
    bool isInstantiated;//slot doluluğunu kontrol etmek için

    TextMeshProUGUI amountText;

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();
    //string sözel şeyler(itemlerın isimleri), int ise değerlerimiz 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSlotsAvailablety(GameObject itemToAdd,string itemName,int itemAmount)
    {
        isInstantiated = false;
        for(int i = 0; i < slots.Length; i++)//tüm slotları almak için yazdık
        {
            if(slots[i].transform.childCount > 0)//slotun childı varsa
            {
                slots[i].GetComponent<Slots>().isUsed = true;//slot isused oluyor
            }
            else if (!isInstantiated && !slots[i].GetComponent<Slots>().isUsed)//slot kullanılmamışsa (boşsa, instantiate edilmemiş ve isused değilse)
            {
                if(!inventoryItems.ContainsKey(itemName))//stacklenebilir bir item değilse(itemname'i aynı olan bir iteme denk gelmediysek)
                {
                    GameObject item = Instantiate(itemToAdd, slots[i].transform.position, Quaternion.identity);//itemi yarat
                    item.transform.SetParent(slots[i].transform, false);//slotun altında children olarak yarat
                    item.transform.localPosition = new Vector3 (0, 0, 0);//locationu bir üst satırda false yapıp kendimiz locationu merkezde oluşturduk
                    item.name = item.name.Replace("(Clone)", "");
                    isInstantiated = true;//instantiate ettiğimiz için true olarak set ediyoruz
                    inventoryItems.Add(itemName, itemAmount);//son olarak sözlüğe ekliyoruz(ismini ve adedini) 
                    amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    amountText.text = itemAmount.ToString();
                    break;
                }
                else//stacklenebilir item ise 
                {
                    for(int j = 0; j < slots.Length; i++)
                    {
                        if(slots[j].transform.GetChild(0).gameObject.name == itemName)//slotun childinın ismi itemin ismiyle aynıysa
                        {
                            inventoryItems[itemName] += itemAmount;//itemi ekle
                            amountText.text = inventoryItems[itemName].ToString();
                            break; 
                        }
                    }
                    break;
                }
            }
        }
    }

    public void UseInventoryItems(string itemName)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(!slots[i].GetComponent<Slots>().isUsed)
            {
                continue;
            }
            if(slots[i].transform.GetChild(0).gameObject.name == itemName)//eğer slotun altındaki childın ismi itemnameye eşit ise 
            {
                inventoryItems[itemName]--;
                amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                amountText.text = inventoryItems[itemName].ToString();

                if(inventoryItems[itemName] <= 0)//item bittiyse
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);//childı yok et
                    slots[i].GetComponent<Slots>().isUsed = false;//isused değildir 
                    inventoryItems.Remove(itemName);//itemi kaldır
                    ReorganizedInv();
                }
                break;
            }
        }
    }

    public void ReorganizedInv()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(!slots[i].GetComponent<Slots>().isUsed)
            {
                for(int j = i + 1; j < slots.Length; j++)
                {
                    if(slots[j].GetComponent<Slots>().isUsed)
                    {
                        Transform itemMove = slots[j].transform.GetChild(0).transform;
                        itemMove.transform.SetParent(slots[i].transform, false);
                        itemMove.transform.localPosition = new Vector3 (0, 0, 0);
                        slots[i].GetComponent<Slots>().isUsed = true;
                        slots[j].GetComponent<Slots>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }
}
