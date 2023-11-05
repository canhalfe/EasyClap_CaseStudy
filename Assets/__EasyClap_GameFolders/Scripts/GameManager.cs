using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        UpdateInGameMetrics();
    }

    public void UpdateInGameMetrics()
    {
        FireRate = gameData.fireRateList[gameData.fireRateID];
        Distance = gameData.rangeList[gameData.rangeID];
        Power = gameData.damageList[gameData.damageId];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanPlay)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!hit.transform.CompareTag("GameStart")) return;
                hit.transform.gameObject.SetActive(false);
                gameStart = true;
                uiManager.tutorialImage.gameObject.SetActive(false);
                uiManager.gameStartUI.SetActive(false);
                uiManager.tutorialImage.gameObject.SetActive(false);
            }
        }
    }

    public void LevelFinished()
    {
        gameStart = false;
        CanPlay = false;
        DOVirtual.DelayedCall(.5f, () => uiManager.levelEndUI.SetActive(true));
        DOVirtual.DelayedCall(1f, () => uiManager.nextBtn.gameObject.SetActive(true));
        Camera.main.GetComponent<CameraFollowNew>().SmootFactor = 5;
        
    }

    public void NextLvl()
    {
        gameData.levelId++;
        if (SceneManager.sceneCountInBuildSettings <= gameData.levelId)
        {
            int rndScene = Random.Range(0, 3);
            SceneManager.LoadScene(rndScene);
        }
        else
            SceneManager.LoadScene(gameData.levelId-1);
    }
}
