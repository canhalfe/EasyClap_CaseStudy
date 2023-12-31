﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    ObjectPoolManager objectPoolManager;
    AudioSource audioSource;
    private float timer;

    void Start()
    {
        gameManager = GameManager.Instance;
        objectPoolManager = ObjectPoolManager.Instance;
        audioSource = GetComponent<AudioSource>();
        timer = gameManager.FireRate;
    }

    void Update()
    {
        if (!gameManager.gameStart || !gameManager.CanPlay) return;

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
            //transform.parent.GetComponent<Animator>().SetTrigger("Shot");
            transform.parent.parent.GetComponent<Animator>().SetTrigger("Shoot");
            audioSource.Play();
        }
    }
}
