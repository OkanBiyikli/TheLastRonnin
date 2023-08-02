using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float timer;
    public float damage;
    public float expToGive;

    public AudioSource hitAS, deadAS;
    public GameObject deathEffect;
    HitEffect effect;
    Rigidbody2D rb;

    public Transform player;
    public float knockBackForceX, knockBackForceY;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        effect = GetComponent<HitEffect>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        AudioManager.instance.PlayAudio(hitAS);

        if(player.position.x < transform.position.x)//player sol tarafta ise (düşmana göre)
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }else//player sağ tarafta ise bu forceu uygula
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        
        //GetComponent<SpriteRenderer>().material = effect.white;//hasar aldığım zaman spprite rendererı getir onun altındaki materialı hiteffectteki whitea eşitle
        //StartCoroutine(BackToNormal());
        if(currentHealth <= 0)//enemynin öldüğü kısım
        {
            currentHealth = 0;
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Experience.instance.expMod(expToGive);//experience scriptindeki expmoda exptogive kadar ekle
            AudioManager.instance.PlayAudio(deadAS);
        }
    }

   /* IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);//bekleme süresi
        GetComponent<SpriteRenderer>().material = effect.original;
    }*/
}
