using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Obstacles
{
    Wall,
    Barrel
}
public class GameManager : Singleton<GameManager>
{
    public GameData gameData;
    public bool gameStart;
    public int gunID;
    void Start()
    {
        
    }

}
