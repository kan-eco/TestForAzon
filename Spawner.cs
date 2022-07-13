using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform spawn1;

    [SerializeField]
    Transform spawn2;

    [SerializeField]
    Transform spawn3;

    [SerializeField]
    GameObject Bandit;

    GameObject _bandit;

    public float RespawnSpeedPerSec = 3;
    float timer = 0;
    int damIncrease = 0;
    float damTimer = 10;
    int randomPlace;

    void Start()
    {
        _bandit = Instantiate(Bandit, spawn1.position, Quaternion.identity);

        _bandit = Instantiate(Bandit, spawn2.position, Quaternion.identity);
    }


    void Update()
    {
        timer += Time.deltaTime;
        damTimer -= Time.deltaTime;

        if (damTimer <= 0)
        {
            damIncrease += 1;
            damTimer = 10;
        }

        if (timer >= RespawnSpeedPerSec)
        {
            timer = 0;

            randomPlace = Random.Range(1,4);

            switch (randomPlace)
            {
                case 1:
                    _bandit = Instantiate(Bandit, spawn1.position, Quaternion.identity);
                    _bandit.GetComponent<Bandit>().atackMight = 1 + damIncrease;
                    break;
                case 2:
                    _bandit = Instantiate(Bandit, spawn2.position, Quaternion.identity);
                    _bandit.GetComponent<Bandit>().atackMight = 1 + damIncrease;
                    break;
                case 3:
                    _bandit = Instantiate(Bandit, spawn3.position, Quaternion.identity);
                    _bandit.GetComponent<Bandit>().atackMight = 1 + damIncrease;
                    break;

                default:
                    break;
            }
        }

    }
}
