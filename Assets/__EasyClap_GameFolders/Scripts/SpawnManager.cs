using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    ObjectPoolManager objectPoolManager;
    //private float fireRate;
    private float timer;
    void Start()
    {
        gameManager = GameManager.Instance;
        objectPoolManager = ObjectPoolManager.Instance;
        gameManager.FireRate = gameManager.gameData.fireRate;
        timer = gameManager.FireRate;
    }

    void Update()
    {
        if (gameManager.gameStart)
            Shoot();
    }

    private void Shoot()
    {
        timer += Time.deltaTime;
        if (timer >= gameManager.FireRate)
        {
            GameObject weapon = objectPoolManager.GetPool(gameManager.gunID);
            weapon.transform.position = transform.position;
            weapon.GetComponent<WeaponController>().spPoint = transform;
            timer = 0;
            transform.parent.GetComponent<Animator>().SetBool("Shoot", true);
        }
        else
            transform.parent.GetComponent<Animator>().SetBool("Shoot", false);
    }
}
