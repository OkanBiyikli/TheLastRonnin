using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStats : MonoBehaviour
{
    float attackPower;
    float totalAttack;
    public float weaponAttack;

    public GameObject damageText;

    PlayerController player;
    void Start()
    {
        player = GetComponent<PlayerController>();
        attackPower = player.damage;//attackpower dediğimiz şey playerdaki damagee eşit olucak
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DamageInput()
    {
        totalAttack = attackPower + weaponAttack;
        float finalAttack = Mathf.Round(Random.Range(totalAttack - 5, totalAttack + 5));//damagemiz +5 -5 arasında random vursun
        GameObject textDam = Instantiate(damageText, new Vector2(transform.position.x + 1, transform.position.y + 1), Quaternion.identity);
        textDam.GetComponent<TextMeshPro>().SetText(finalAttack.ToString());
        
        if(finalAttack > totalAttack + 3)//eğer totalatacka eklediğimiz değer +4 ve +5 ise kritik vursun son damageyi 2ile çarpsın
        {
            textDam.GetComponent<TextMeshPro>().SetText("CRITICAL!\n" + finalAttack.ToString());
            finalAttack *= 2;
        }
        return finalAttack;
    }
}
