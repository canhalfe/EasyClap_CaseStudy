using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager _gameManager;
    GameData _gameData;

    [Header("Coin")]
    public Image coinImage;
    public TextMeshProUGUI coinText;

    [Space]
    [Header("Tutorial")]
    public Image tutorialImage;
    public GameObject gameStartUI;

    [Space]
    [Header("Buy Buttons UI")]
    public TextMeshProUGUI fireRateButtonPriceText;
    public TextMeshProUGUI rangeButtonPriceText;
    public TextMeshProUGUI damageButtonPriceText;
    [Header("--------------------------")]
    public TextMeshProUGUI fireRateButtonLevelText;
    public TextMeshProUGUI rangeButtonLevelText;
    public TextMeshProUGUI damageButtonLevelText;
    [Space]
    [Header("Level End")]
    public GameObject levelEndUI;
    public Button nextBtn;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameData = _gameManager.gameData;
        RefreshCoinText();
        RefreshButtonPriceText();
        RefreshButtonLevelText();
        CheckButtonLevelMaxOrNot();
    }

    public void RefreshCoinText()
    {
        if (_gameManager.gameData.totalCoin < 0) _gameManager.gameData.totalCoin = 0;
        coinText.text = _gameData.totalCoin.ToString();
        ChangeTextType(coinText, _gameData.totalCoin);
    }

    private void ChangeTextType(TextMeshProUGUI text, float value)
    {
        if (value > 1000)
            text.text = (value / 1000).ToString("0.0#K").Replace(",", ".");
        else
            text.text = value.ToString("F0").Replace(",", ".");
    }

    public void IncreaseFireRate()
    {
        if (_gameData.totalCoin < _gameData.fireRatePrices[_gameData.fireRateID])
            return;

        _gameData.totalCoin -= _gameData.fireRatePrices[_gameData.fireRateID];
        _gameData.fireRateID++;
        RefreshButtonPriceText();
        ChangeTextType(fireRateButtonPriceText, _gameData.fireRatePrices[_gameData.fireRateID]);
        _gameManager.UpdateInGameMetrics();
        RefreshButtonLevelText();
        CheckButtonLevelMaxOrNot();
        fireRateButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<ParticleSystem>().Play();
        fireRateButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<AudioSource>().Play();
    }

    public void IncreaseRange()
    {
        if (_gameData.totalCoin < _gameData.rangePrices[_gameData.rangeID]) return;
        _gameData.totalCoin -= _gameData.rangePrices[_gameData.rangeID];
        _gameData.rangeID++;
        RefreshButtonPriceText();
        ChangeTextType(rangeButtonPriceText, _gameData.rangePrices[_gameData.rangeID]);
        _gameManager.UpdateInGameMetrics();
        RefreshButtonLevelText();
        CheckButtonLevelMaxOrNot();
        rangeButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<ParticleSystem>().Play();
        rangeButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<AudioSource>().Play();
    }

    public void IncreaseDamage()
    {
        if (_gameData.totalCoin < _gameData.damagePrices[_gameData.damageId]) return;
        _gameData.totalCoin -= _gameData.damagePrices[_gameData.damageId];
        _gameData.damageId++;
        RefreshButtonPriceText();
        ChangeTextType(damageButtonPriceText, _gameData.damagePrices[_gameData.damageId]);
        _gameManager.UpdateInGameMetrics();
        RefreshButtonLevelText();
        CheckButtonLevelMaxOrNot();
        damageButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<ParticleSystem>().Play();
        damageButtonLevelText.transform.parent.parent.GetChild(1).GetComponent<AudioSource>().Play();
    }

    private void RefreshButtonLevelText()
    {
        fireRateButtonLevelText.text = "LEVEL " + (_gameData.fireRateID + 1);
        rangeButtonLevelText.text = "LEVEL " + (_gameData.rangeID + 1);
        damageButtonLevelText.text = "LEVEL " + (_gameData.damageId + 1);
    }

    private void RefreshButtonPriceText()
    {
        fireRateButtonPriceText.text = _gameData.fireRatePrices[_gameData.fireRateID].ToString();
        rangeButtonPriceText.text = _gameData.rangePrices[_gameData.rangeID].ToString();
        damageButtonPriceText.text = _gameData.damagePrices[_gameData.damageId].ToString();
        RefreshCoinText();
    }

    private void CheckButtonLevelMaxOrNot()
    {
        if (_gameData.fireRateID + 1 >= _gameData.fireRateList.Count)
        {
            fireRateButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;
            fireRateButtonLevelText.text = "MAX";
        }
        if (_gameData.totalCoin < _gameData.fireRatePrices[_gameData.fireRateID])
            fireRateButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;

        if (_gameData.rangeID + 1 >= _gameData.rangeList.Count)
        {
            rangeButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;
            rangeButtonLevelText.text = "MAX";
        }
        if (_gameData.totalCoin < _gameData.rangePrices[_gameData.rangeID])
            rangeButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;

        if (_gameData.damageId + 1 >= _gameData.damageList.Count)
        {
            damageButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;
            damageButtonLevelText.text = "MAX";
        }
        if (_gameData.totalCoin < _gameData.damagePrices[_gameData.damageId])
            damageButtonLevelText.transform.parent.GetComponent<Button>().interactable = false;
        //return;
    }
}
