using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    ParticlesController particlesController;
    public Obstacles obstacles;
    public float health;
    void Start()
    {
        particlesController = GetComponent<ParticlesController>();
        if (health <= 0) health = 5; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            health -= other.GetComponent<WeaponController>().Power;
            if (health <= 0) Blow();
        }
    }

    private void Blow()
    {
        if (particlesController.particleList.Count == 0) return;

        switch (obstacles)
        {
            case Obstacles.Wall:
                particlesController.PlayFX(transform.position, 0);
                break;
            case Obstacles.Barrel:
                particlesController.PlayFX(transform.position, 1);
                break;
        }
        gameObject.SetActive(false);
    }
}
