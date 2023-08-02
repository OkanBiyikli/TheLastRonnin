using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    bool isImmune;
    public float immunetyTime;
    public Image healthBar;

    /*public float knockBackForceX, knockBackForceY;//deneme
    public Transform enemy;//deneme
    Rigidbody2D rb;//deneme*/

    Animator anim;

    public static PlayerHealth instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        /*rb = GetComponent<Rigidbody2D>();*/
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", currentHealth);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.fillAmount = currentHealth / maxHealth;

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Enemy") && !isImmune)
        {
            currentHealth -= collision.GetComponent<EnemyStats>().damage;
            StartCoroutine(Immunity());
            anim.SetTrigger("Hit");
            
           /* if(enemy.position.x < transform.position.x)    //deneme 
            {
                rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }else
            {
                rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }*/
            
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunetyTime);
        isImmune = false;
    }
}
