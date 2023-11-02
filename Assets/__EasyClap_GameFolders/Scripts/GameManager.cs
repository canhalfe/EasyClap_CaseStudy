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

public class GameManager : Singleton<GameManager>
{
    public GameData gameData;
    public bool gameStart;
    public int gunID;

    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) gameStart = true;
    }
}
