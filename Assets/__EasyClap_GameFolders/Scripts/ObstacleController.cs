using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    GameManager gameManager;
    ParticlesController particlesController;
    [SerializeField]private List<Transform> brickList = new();
    public Obstacles obstacles;
    public float health;
    private float price;

    [Header("GateUI")]
    public TextMeshProUGUI healthText;

    void Start()
    {
        gameManager = GameManager.Instance;
        particlesController = ParticlesController.Instance;
        if (health <= 0) health = 3;
        price = health * 10;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            health -= gameManager.Power;
            health = Mathf.Round(health);
            UpdateHealthText();
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
            int rnd = UnityEngine.Random.Range(-1, 1);
            int rnd2 = UnityEngine.Random.Range(100, 250);
            brick.AddComponent<BoxCollider>();
            rb.useGravity = true;
            rb.AddForce(Vector3.forward * rnd2 + Vector3.up * rnd2 + Vector3.right * rnd * rnd2/2);
            brickList.Remove(brick);
            GetComponent<AudioSource>().Play();
        }
    }

    private void Blow()
    {
        SpawnCoin();
        gameObject.SetActive(false);

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
