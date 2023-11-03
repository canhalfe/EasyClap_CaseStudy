using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameData _gameData;

    [Header("Coin")]
    public Image coinImage;
    public TextMeshProUGUI coinText;
    [Header("Tutorial")]
    public Image tutorialImage;

    void Start()
    {
        _gameData = GameManager.Instance.gameData;
        RefreshCoinText();
    }

    public void RefreshCoinText()
    {
        coinText.text = _gameData.totalCoin.ToString();
        ChangeTextType(coinText, _gameData.totalCoin);
    }

    private void ChangeTextType(TextMeshProUGUI text, float value)
    {
        if (_gameData.totalCoin > 1000)
            text.text = (value / 1000).ToString("0.0#K").Replace(",", ".");
        else
            text.text = value.ToString("F0").Replace(",", ".");
    }
}
