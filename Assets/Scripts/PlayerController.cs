using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Collider2D playerCollider;
    public Collider2D disColl;
    public float speed;
    public float jumpForce;
    public LayerMask ground;

    public int cherryNum;

    public TMP_Text numberText;

    private bool isHurt;

    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        // left/right move
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            animator.SetFloat("Running", Mathf.Abs(horizontalMove));
        }
        // update player face direction
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (faceDirection != 0)
            transform.localScale = new Vector3(faceDirection, 1.0f, 1.0f);

        // player jump
        if (Input.GetButton("Jump"))
        {
            if (animator.GetBool("Jumping") == false && animator.GetBool("Falling") == false)
            {
                SoundManager.instance.JumpAudio();

                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
                animator.SetBool("Jumping", true);
            }
        }


        Crouch();
    }

    void SwitchAnim()
    {
        animator.SetBool("Idle", false);

        if (animator.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", true);
            }
        }
        else if (animator.GetBool("Falling") && playerCollider.IsTouchingLayers(ground))
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Idle", true);
        }
        else if (isHurt)
        {
            animator.SetBool("GetHurt", true);
            animator.SetFloat("Running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                animator.SetBool("GetHurt", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);

            SoundManager.instance.CherryAudio();

            cherryNum += 1;

            numberText.SetText(cherryNum.ToString());
        }
        else if (collision.tag == "DeadLine")
        {
            SoundManager.instance.DisableAudio();
            Invoke("Restart", 2.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy forg = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("Falling"))
            {
                forg.JumpOn();

                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2 * Time.fixedDeltaTime);
                animator.SetBool("Jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);

                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);

                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(head.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                animator.SetBool("Crouching", true);
                disColl.enabled = false;
            }
            else
            {
                animator.SetBool("Crouching", false);
                disColl.enabled = true;
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
