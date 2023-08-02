using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    private float movementDirection;
    public float speed;
    public float jumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0;
    public float damage;

    private bool isFacingRight = true;
    private bool isGrounded;

    public AudioSource swordAS;

    Rigidbody2D rb;
    Animator anim;
    WeaponStats weaponStat;
    
    public GameObject inventory;
    bool invIsActive = false;//inventorymiz başlangıçta kapalı olacağı için false yaptık

    public GameObject groundCheck;

    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayers;

    public GameObject ninjaStar;//ninjastarı fırlatmak için sahneye çağırıcağımızdan dolayı bu nesneyi belirliyoruz
    public Transform firePoint;

    public static PlayerController instance;


    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponStat = GetComponent<WeaponStats>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();
        AttackInput();
        Roll();
        Shoot();
        InventoryOpen();
    }

    private void FixedUpdate() 
    {
        Movement();
    }

    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));//koşma animasyonu için animatorden verdiğimiz runspeed float değerini referans alarak yazıyoruz 
                                //mutlak değeri kullanmamızın sebebi sağa sola dönerken koşma animasyonunda sıkıntı olmasın diye
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void CheckRotation()
    {
        if(isFacingRight && movementDirection < 0)//yüzümüz sağa dönük ve ben sola bastıysam
        {
            Flip();
        }else if(!isFacingRight && movementDirection > 0)//yüzümüz sola dönük ve ben sağa bastıysam
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;//true false mantığı oldıuğu için biri true veya falseken diğerine etki etsin diye
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;//scaleyi her bu işlem yapıldığında -1 ile çarp
        transform.localScale = theScale; 
    }

    void Jump()
    {
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            } 
        }
        
    }

    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius); 
    }

    public void Attack()
    {
        float numb = Random.Range(0, 2);
        if(numb == 0)
        {
            anim.SetTrigger("Attack1");
            AudioManager.instance.PlayAudio(swordAS);
        }
        if(numb == 1)
        {
            anim.SetTrigger("Attack2");
            AudioManager.instance.PlayAudio(swordAS);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(weaponStat.DamageInput());
        }
    }

    public void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(StarBank.instance.bankStar > 0)
            {
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                StarBank.instance.bankStar -= 1;
                //PlayerPrefs.SetInt("StarAmount", StarBank.instance.bankStar);
            }
        }
    }

    public void AttackInput()
    {
        if(Time.time > nextAttack)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        
    }

    public void Roll()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            anim.SetTrigger("Roll");
        }
    }

    public void InventoryOpen()
    {
        if(Input.GetKeyDown(KeyCode.I) && !invIsActive)
        {
            inventory.SetActive(true);
            invIsActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && invIsActive)
        {
            inventory.SetActive(false);
            invIsActive = false;
        }
    }
}
