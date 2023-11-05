using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Game Control")]
    public int levelId;

    [Space]
    [Header("Game Metrics")]
    public float totalCoin;
    public int fireRateID;
    public int rangeID;
    public int damageId;

    [Space]
    [Space]
    [Header("Buy Button Metrics")]
    public List<float> fireRateList = new();
    public List<float> rangeList = new();
    public List<float> damageList = new();

    [Space]
    [Space]
    [Header("Buy Button Prices")]
    public List<float> fireRatePrices = new();
    public List<float> rangePrices = new();
    public List<float> damagePrices = new();

    [Button]
    public void ResetGameData()
    {
        totalCoin = 0;
        levelId = 1;
        fireRateID = 0;
        rangeID = 0;
        damageId = 0;
    }
}

