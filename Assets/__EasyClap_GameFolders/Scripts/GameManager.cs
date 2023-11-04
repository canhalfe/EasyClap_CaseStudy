using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Obstacles
{
    Wall,
    Barrel
}
//public enum WeaponType
//{
//    Kunai,
//    Shuriken,
//    Pistol
//}
public enum GateTypes
{
    FireRate,
    Range,
    Power
}
public enum GateColor
{
    Green,
    Red
}

public class GameManager : Singleton<GameManager>
{
    public GameData gameData;
    public UIManager uiManager;
    public bool gameStart;
    public int gunID;
    [SerializeField] private float fireRate;
    [SerializeField] private float distance;
    [SerializeField] private float power;
    private bool canPlay = true;
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float Distance { get => distance; set => distance = value; }
    public float Power { get => power; set => power = value; }
    public bool CanPlay { get => canPlay; set => canPlay = value; }

    void Start()
    {
        FireRate = gameData.fireRate;
        Distance = gameData.distance;
        Power = gameData.power;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanPlay)
        {
            gameStart = true;
            uiManager.tutorialImage.gameObject.SetActive(false);
        }
    }


}
