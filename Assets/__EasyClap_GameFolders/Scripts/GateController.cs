using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateController : MonoBehaviour
{
    GameManager gameManager;
    public GateTypes gateTypes;
    public GateColor gateColor;
    public float gateValue;
    public float gateIncreaseValue;
    public GameObject gate;
    public GameObject digitSlot;
    public GameObject glow;

    [Header("GateMaterials")]
    public List<Material> gateMatRedList = new();
    public List<Material> gateMatGreenList = new();

    [Header("GateUI")]
    public TextMeshProUGUI gateName;
    public TextMeshProUGUI gateIncreaseValueText;
    public TextMeshProUGUI gateValueText;

    void Start()
    {
        gameManager = GameManager.Instance;
        switch (gateTypes)
        {
            case GateTypes.FireRate:
                gateName.text = "FIRE RATE";
                break;
            case GateTypes.Range:
                gateName.text = "RANGE";
                break;
            case GateTypes.Power:
                gateName.text = "POWER";
                break;
        }
        gateValue *= -1;
        RefreshGateValues();
        RefreshGateColor();
    }

    private void RefreshGateColor()
    {
        if (gateValue < 0)
            gateColor = GateColor.Red;
        else
            gateColor = GateColor.Green;

        switch (gateColor)
        {
            case GateColor.Green:
                gate.GetComponent<MeshRenderer>().material = gateMatGreenList[0];
                digitSlot.GetComponent<MeshRenderer>().material = gateMatGreenList[0];
                glow.GetComponent<MeshRenderer>().material = gateMatGreenList[1];
                break;
            case GateColor.Red:
                gate.GetComponent<MeshRenderer>().material = gateMatRedList[0];
                digitSlot.GetComponent<MeshRenderer>().material = gateMatRedList[0];
                glow.GetComponent<MeshRenderer>().material = gateMatRedList[1];
                break;
        }
    }

    private void RefreshGateValues()
    {
        gateIncreaseValueText.text = gateIncreaseValue.ToString();
        gateValueText.text = gateValue.ToString();
        RefreshGateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            ChangeDoorValue();
        }
    }

    private void ChangeDoorValue()
    {
        gateValue += gateIncreaseValue;
        RefreshGateValues();
    }
}
