using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]GameManager gameManager;
    ObjectPoolManager poolManager;
    [SerializeField]private float distance = 10;
    private float power;
    private float gunID;
    public Transform spPoint;

    public float Power { get => power; set => power = value; }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        poolManager = ObjectPoolManager.Instance;
        distance = gameManager.gameData.distance;
        Power = gameManager.gameData.power;
        gunID = gameManager.gunID;
    }

    void Update()
    {
        float tempDistance = 0;
        tempDistance = transform.position.z - spPoint.position.z;
        if (tempDistance >= distance)
        {
            GetBackToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") || other.CompareTag("Obstacle"))
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
