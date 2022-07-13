using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeart : MonoBehaviour
{
    float timer = 1;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer >= 0)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }
        else { transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime; }

        if (timer <= -1) {timer = 1;}
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<KnightController>())
        {
            collision.gameObject.GetComponent<KnightController>().KnightHealth(-1);
            Destroy(gameObject);
        }
    }
}
