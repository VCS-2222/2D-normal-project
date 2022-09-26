using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float currentSpeed;
    float walkSpeed = 3f;
    float runSpeed = 6f;
    public Rigidbody2D rb;

    void Start()
    {
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude > currentSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, currentSpeed);
            //rb.velocity.Normalize();
        }
    }

    void MyInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * currentSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * currentSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * currentSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * currentSpeed);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}
