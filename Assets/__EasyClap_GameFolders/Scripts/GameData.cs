using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Game Data")]
public class GameData : ScriptableObject
{
    public float totalCoin;
    public float fireRate;
    public float distance;
    public float power;

    [Header("Game Control")]
    public int levelId;
}
