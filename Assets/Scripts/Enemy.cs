using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        audioSource.Play();
        animator.SetTrigger("Death");
    }
}
