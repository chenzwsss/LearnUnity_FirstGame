using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    private Rigidbody2D rb;

    public Transform leftPoint, rightPoint;

    private float leftPosX, rightPosX;

    public float speed = 10.0f;
    public float jumpForce = 10.0f;

    public bool faceLeft = true;

    public LayerMask ground;
    private Collider2D coll;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftPosX = leftPoint.position.x;
        rightPosX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveMement();
        SwitchAnim();
    }

    void Movement()
    {
        if (faceLeft)
        {
            if (transform.position.x <= leftPosX)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                faceLeft = false;
            }
            else
            {
                if (coll.IsTouchingLayers(ground))
                {
                    animator.SetBool("Jumping", true);
                    rb.velocity = new Vector2(-speed, jumpForce);
                }
            }
        }
        else
        {
            if (transform.position.x >= rightPosX)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                faceLeft = true;
            }
            else
            {
                if (coll.IsTouchingLayers(ground))
                {
                    animator.SetBool("Jumping", true);
                    rb.velocity = new Vector2(speed, jumpForce);
                }
            }
        }
    }

    void SwitchAnim()
    {
        if (animator.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", true);
            }
        }

        if (animator.GetBool("Falling") && coll.IsTouchingLayers(ground))
        {
            animator.SetBool("Falling", false);
        }
    }
}
