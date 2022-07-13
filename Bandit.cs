using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    SpriteRenderer renderer;
    Animator animator;

    GameObject target;

    public int speed = 3;
    public int atackMight = 1;

    public float distToLook = 15;
    public float distToAttack = 1.5f;

    bool atk = false;
    float timer = 0.85f;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        if (target.transform.position.x >= transform.position.x)
        {
            renderer.flipX = true;
        }

        if (target.transform.position.x < transform.position.x)
        {
            renderer.flipX = false;
        }

        

        if (Vector3.Distance(target.transform.position, transform.position) <= distToLook && !atk)
        {
            animator.SetBool("run", true);
            transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * speed);

        }
        

        if (Vector3.Distance(target.transform.position, transform.position) <= distToAttack)
        {
            atk = true;
            animator.SetBool("run", false);
            animator.SetBool("attack", true);
        }

        if (Vector3.Distance(target.transform.position, transform.position) >= distToAttack)
        {
            atk = false;
            animator.SetBool("attack", false);
        }

    }


    void Attack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= distToAttack && atk == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                target.GetComponent<KnightController>().KnightHealth(atackMight);
                timer = 0.85f;
            }
        }
    }
}
