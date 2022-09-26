using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Variables")]
    public float currentSpeed;
    float runSpeed = 140f;
    float attackSpeed = 0f;
    public Rigidbody2D rb;
    public SpriteRenderer pyrSprite;
    public Animator animator;
    bool isRunning = false;
    public bool isGrounded = false;
    public bool isjumping = false;
    public bool isAttacking1 = false;
    public bool canAttack2 = false;
    public bool isAttacking2 = false;

    void Start()
    {
        currentSpeed = runSpeed;
    }

    void Update()
    {
        MyInput();
        CheckRun();
        AttackCheck();
    }

    private void FixedUpdate()
    {
        rb.velocity.Normalize();
    }

    void CheckRun()
    {
        if(isRunning)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    void MyInput()
    {
        if (isjumping)
            return;

        if (isAttacking1)
            return;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * currentSpeed);
            pyrSprite.flipX = true;
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * currentSpeed);
            pyrSprite.flipX = false;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isAttacking1 && !isAttacking2)
        {
            StartCoroutine(jumping());
        }
    }

    IEnumerator jumping()
    {
        isjumping = true;
        rb.AddForce(transform.up * 40000f);
        animator.SetTrigger("jump");

        yield return new WaitForSeconds(0.5f);

        rb.AddForce(-transform.up * 25000f);
        isjumping = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true; 
        }
    }

    void AttackCheck()
    {
        if(isGrounded && !isjumping)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(attack1());
            }

            if(canAttack2 && Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(attack2());
            }
            else
            {
                currentSpeed = runSpeed;
            }
        }
    }

    IEnumerator attack1()
    {
        animator.SetTrigger("attack1");
        currentSpeed = attackSpeed;
        isAttacking1 = true;
        isRunning = false;

        yield return new WaitForSeconds(0.2f);

        currentSpeed = runSpeed;
        isAttacking1 = false;
        canAttack2 = true;
    }

    IEnumerator attack2()
    {
        animator.SetTrigger("attack2");
        currentSpeed = attackSpeed;
        isAttacking2 = true;
        canAttack2 = false;

        yield return new WaitForSeconds(0.2f);

        isAttacking2 = false;
        canAttack2 = false;
        currentSpeed = runSpeed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
