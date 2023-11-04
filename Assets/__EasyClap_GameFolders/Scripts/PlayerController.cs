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

        if (other.CompareTag("Obstacle")) ThrowYourselfBack(other.transform);

        if (other.CompareTag("Door")) UpdateInGameSkills(other.transform);   
        
        if (other.CompareTag("Chest")) LevelFinished();   
    }

    private void UpdateInGameSkills(Transform gate)
    {
        GateController gateController = gate.GetComponent<GateController>();
        gate.GetComponent<BoxCollider>().enabled = false;
        switch (gateController.gateTypes)
        {
            case GateTypes.FireRate:
                gameManager.FireRate -= gateController.gateValue;
                break;
            case GateTypes.Range:
                gameManager.Distance += gateController.gateValue;
                break;
            case GateTypes.Power:
                gameManager.Power += gateController.gateValue;
                gameManager.Power = Mathf.Round(gameManager.Power);
                break;
        }
    }

    private void ThrowYourselfBack(Transform obstacle)
    {
        transform.parent.transform.DOMoveZ(transform.position.z - 2.2f, .3f).SetEase(Ease.Flash).SetUpdate(UpdateType.Fixed);
        obstacle.gameObject.layer = 11;
    }

    private void CollectCoin(Transform coin)
    {
        ParticlesController.Instance.PlayFX(coin.position, 3);
        gameManager.gameData.totalCoin += coin.GetComponentInParent<CoinController>().Price;
        coin.gameObject.SetActive(false);
        gameManager.uiManager.RefreshCoinText();
    }

    private void LevelFinished()
    {
        gameManager.gameStart = false;
        gameManager.CanPlay = false;
    }
}
