using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    GameManager gameManager;
    ObjectPoolManager poolManager;

    public Transform spPoint;
    private float gunID;

    void Start()
    {
        gameManager = GameManager.Instance;
        poolManager = ObjectPoolManager.Instance;
        
        gunID = gameManager.gunID;
    }

    void Update()
    {
        float tempDistance = transform.position.z - spPoint.position.z;
        if (tempDistance >= gameManager.Distance)
        {
            GetBackToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") || other.CompareTag("Obstacle") || other.CompareTag("Chest"))
        {
            GetBackToPool();
        }
    }

    private void GetBackToPool()
    {
        poolManager.AddPool(gameObject, gameManager.gunID);
        transform.parent = null;
    }
}
