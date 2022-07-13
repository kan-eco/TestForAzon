using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int _health;

    [SerializeField]
    GameObject Heart;

    [SerializeField]
    GameObject bar;

    float barStep;
    float curBar;

    Animator animator;
    Bandit banditScript;

    
    GameObject manager;


    private void Start()
    {
        _health = maxHealth;

        animator = GetComponent<Animator>();
        banditScript = GetComponent<Bandit>();
        manager = GameObject.FindGameObjectWithTag("GameController");

        barStep = (float)1 / maxHealth;
    }
        

    void Update()
    {
        

        if (_health <= 0)
        {
            
            banditScript.enabled = false;
            animator.Play("Die");


            StartCoroutine("ImDie");
        }

        if (_health > maxHealth) {_health = maxHealth; }
    }

    public void MyLife(int points)
    {
        _health -= points;
        curBar = _health * bar.transform.localScale.x * barStep;

        bar.transform.localScale = new Vector3(curBar, 0.1f, 0);
        Debug.Log(curBar);
    }

    IEnumerator ImDie()
    {
        yield return new WaitForSeconds(1);
        manager.GetComponent<Manager>().Score(1);
        Instantiate(Heart, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
