using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin")) CollectCoin(other.transform);

        if (other.CompareTag("Obstacle")) ThrowYourselfBack();
    }

    private void ThrowYourselfBack()
    {
        transform.parent.transform.DOMoveZ(transform.position.z - 2.2f, .3f).SetEase(Ease.Flash).SetUpdate(UpdateType.Fixed);
    }

    private void CollectCoin(Transform coin)
    {
        coin.gameObject.SetActive(false);
        ParticlesController.Instance.PlayFX(coin.position, 3);
        gameManager.gameData.totalCoin += coin.GetComponent<CoinController>().price;
    }
}
