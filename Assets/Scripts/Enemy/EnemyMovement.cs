using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isPatroller;

    public bool waiter;
    public float waitTime = 1f;
    bool isWaiting;
    public bool isWalkingRight;

    public Transform wallCheck, groundCheck, gapCheck;
    public bool wallDetected, groundDetected, gapDetected;
    public float detectionRadius;
    public LayerMask whatIsGround;

    public Transform pointA, pointB;
    bool moveToA, moveToB;//21 ve 22yi patroller enemy için giriyoruz

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveToA = true;
    }


    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position, detectionRadius, whatIsGround);//fiziksel temas olmadığı için önüne ünlem koyurouz
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGround);


        if(gapDetected || wallDetected && groundDetected)
        {
            Flip();
        }
        
    }

    private void FixedUpdate() 
    {
        if(isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;//static iken rigidbodydeki x y ve z eksenlerinin tamamını dondur
        }

        if(isWalker)
        {       //yürürken z eksenini döndür dedik
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;//rotasyonunu yani z eksenini dondur dedik
            anim.SetBool("Idle", false);
            if(!isWalkingRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }

        if(isPatroller)
        {
            if(!isWaiting)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                anim.SetBool("Idle", false);
            }
            
            if(moveToA)
            {
                
                if(Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    if(waiter)
                    {
                    StartCoroutine(Waiting());
                    }
                    Flip();
                    moveToA = false;
                    moveToB = true;
                }
            }
            
            if(moveToB)
            {
                if(!isWaiting)
                {
                    rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Idle", false);
                }
                
                if(Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    if(waiter)
                    {
                    StartCoroutine(Waiting());
                    }
                    Flip();
                    moveToA = true;
                    moveToB = false;
                }
            }
        }
    }

    public void Flip()
    {
        isWalkingRight = !isWalkingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;//scaleyi her bu işlem yapıldığında -1 ile çarp
        transform.localScale = theScale;
    }

    IEnumerator Waiting()
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();

    }
}
