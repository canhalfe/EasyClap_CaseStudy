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
    [SerializeField]private float fireRate;

    public float FireRate { get => fireRate; set => fireRate = value; }

    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = true;
            uiManager.tutorialImage.gameObject.SetActive(false);
        }
    }
}
