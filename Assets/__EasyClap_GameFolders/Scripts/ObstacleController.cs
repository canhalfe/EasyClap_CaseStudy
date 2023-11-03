using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private List<Transform> brickList = new();
    ParticlesController particlesController;
    public Obstacles obstacles;
    public float health;
    public float price;
    void Start()
    {
        particlesController = ParticlesController.Instance;
        if (health <= 0) health = 3;
        if (price <= 0) price = 10;
        if (obstacles == Obstacles.Wall)
        {
            Transform parent = transform;
            foreach (Transform child in parent)
            {
                brickList.Add(child);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            health -= other.GetComponent<WeaponController>().Power;
            if (health <= 0)
            {
                Blow();
                return;
            };

            if (obstacles == Obstacles.Wall)
                ThrowBricks();
        }
    }

    private void ThrowBricks()
    {
        for (int i = 0; i < (int)((brickList.Count / health) / 2); i++)
        {
            Transform brick = brickList[brickList.Count - 1];
            Rigidbody rb = brick.GetComponent<Rigidbody>();
            int rnd = 0;
            int rnd2 = 0;
            rnd = UnityEngine.Random.Range(-1, 1);
            rnd2 = UnityEngine.Random.Range(100, 250);
            //brick.DOJump
            //    (new Vector3(transform.position.x + rnd, 0.1f, transform.position.z + 1), 2f, 1, 1f).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
            brick.AddComponent<BoxCollider>();
            rb.useGravity = true;
            //rb.mass = 1f;
            rb.AddForce(Vector3.forward * rnd2 + Vector3.up * rnd2 + Vector3.right * rnd * rnd2/2);
            brickList.Remove(brick);
        }
    }

    private void Blow()
    {
        SpawnCoin();
        gameObject.SetActive(false);
        //if (particlesController.particleList.Count <= 0)
        //    return;

        switch (obstacles)
        {
            case Obstacles.Barrel:
                particlesController.PlayFX(transform.position, 0);
                break;
            case Obstacles.Wall:
                particlesController.PlayFX(transform.position, 1);
                break;
        }
        
    }

    private void SpawnCoin()
    {
        GameObject coin = ObjectPoolManager.Instance.GetPool(3);
        coin.GetComponent<CoinController>().Price = price;
        coin.transform.position = transform.position;
        float rnd = 0;
        rnd = UnityEngine.Random.Range(-.5f, .5f);
        coin.transform.DOJump
            (new Vector3(transform.position.x + rnd, 0.55f, transform.position.z + 4), 2f, 1, 1f).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
    }
}
