using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    GameManager gameManager;
    ParticlesController particlesController;
    [SerializeField]Animator animator;
    private float chestHealth;

    [Header("ChestUI")]
    public TextMeshProUGUI chestHealthText;

    void Start()
    {
        gameManager = GameManager.Instance;
        particlesController = ParticlesController.Instance;
        chestHealth = transform.parent.GetComponent<ChestHealth>().chestTripleGroupHealth;
        if (chestHealth <= 0) chestHealth = 1;
        UpdateChestHealthText();
    }

    private void UpdateChestHealthText()
    {
        chestHealthText.text = chestHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            float scaleFactor = 1.1f;
            transform.DOScale(transform.localScale * scaleFactor, .1f).OnComplete(()=> transform.DOScale(transform.localScale / scaleFactor, .1f));
            chestHealth -= gameManager.Power;
            UpdateChestHealthText();
            if (chestHealth <= 0)
            {
                animator.SetTrigger("Open");
                GetComponent<BoxCollider>().enabled = false;
                chestHealthText.transform.parent.gameObject.SetActive(false);
                particlesController.PlayFX(transform.position, 2);
                gameObject.SetActive(false);
                SpawnCoin();
            }
        }
    }

    private void SpawnCoin()
    {
        GameObject coin = ObjectPoolManager.Instance.GetPool(3);
        coin.GetComponent<CoinController>().Price = chestHealth;
        coin.transform.position = transform.position;
        coin.transform.DOJump
            (new Vector3(transform.position.x, 0.55f, transform.position.z + 1),.5f, 1, .5f).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
    }
}
