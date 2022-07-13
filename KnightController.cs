using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [SerializeField]
    HealthBar bar;

    [SerializeField]
    Manager manager;

    SpriteRenderer renderer;

    public Animator animator;
    public int speed = 5;
    public int jumpForce = 400;
    bool onGround = false;
    Vector2 roll;

    Rigidbody2D body;

    public float hitLenght = 2f;

    public Transform point1;
    public Transform point2;
    Transform point;
    Vector3 dir;

    float _health;

    float timer = 0.3f;

    bool blocked = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        roll = new Vector2(0,0);
        _health = bar.maxValue;

    }


    void Update()
    {
        if (blocked == false)
        {
            Movement();
            Attack();
        }

        if (_health <= 0)
        {
            //animator.StopPlayback();
            animator.Play("Die");
            manager.EndGame();
            blocked = true;
        }
    }


    void Movement()

    {
        if (Input.GetKey(KeyCode.A))
        {
            
            renderer.flipX = true;
            animator.SetBool("run", true);
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            renderer.flipX = false;
            animator.SetBool("run", true);
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) { animator.SetBool("run", false); }

        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            animator.SetBool("jump", true);
            body.AddForce(Vector2.up * jumpForce);
            onGround = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && onGround == true)
        {
            if (renderer.flipX == true)
            {
                animator.StopPlayback();
                animator.Play("Roll");
                
                body.AddForce(Vector2.left * jumpForce / 1.5f);
                body.velocity = roll;
            }

            if (renderer.flipX == false)
            {
                animator.StopPlayback();
                animator.Play("Roll");

                body.AddForce(Vector2.right * jumpForce / 1.5f);
                body.velocity = roll;
            }
        }

    }

    void Attack()
    {


        if (Input.GetMouseButton(0))
        {
            animator.SetBool("attack", true);
            timer -= Time.deltaTime;
            if (timer < 0) {timer = 0 ;}

            if (renderer.flipX == true) 
            {
                point = point2;
                dir = - point.right;
            }

            if (renderer.flipX == false)
            {
                point = point1;
                dir = point.right;
            }

            RaycastHit2D hit = Physics2D.Raycast(point.position,dir,hitLenght);

            if (hit && hit.transform.GetComponent<EnemyHealth>() && timer == 0)
            {
                hit.transform.GetComponent<EnemyHealth>().MyLife(1);
                timer = 0.3f;
            }

        }

        else
        {
            animator.SetBool("attack", false);
        }


    }


    public void KnightHealth( int points)
    {
        _health -= points;
        bar.PlayerHealth(-points);

        if (_health < 0) {_health = 0;}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onGround = true;
            animator.SetBool("jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onGround = false;
            animator.SetBool("jump", true);


        }
    }

}
