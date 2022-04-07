using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumController : Enemy
{
    public Transform leftPoint, rightPoint;
    private float leftPosX, rightPosX;

    private Rigidbody2D rb;

    public float speed;

    private bool faceLeft = true;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        transform.DetachChildren();

        leftPosX = leftPoint.position.x;
        rightPosX = rightPoint.position.x;

        Debug.Log(leftPosX.ToString());
        Debug.Log(rightPosX.ToString());

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceLeft)
        {
            if (transform.position.x > leftPosX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                faceLeft = false;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            if (transform.position.x < rightPosX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                faceLeft = true;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}
