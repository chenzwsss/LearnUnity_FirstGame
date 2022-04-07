using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    private Rigidbody2D rb;
    public Transform upPoint, downPoint;

    private float upPosY, downPosY;

    public float speed = 1.5f;

    private bool flyUp = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        transform.DetachChildren();
        upPosY = upPoint.position.y;
        downPosY = downPoint.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (flyUp)
        {
            if (transform.position.y < upPosY)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else
            {
                flyUp = false;
            }
        }
        else
        {
            if (transform.position.y > downPosY)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
            {
                flyUp = true;
            }
        }
    }
}
