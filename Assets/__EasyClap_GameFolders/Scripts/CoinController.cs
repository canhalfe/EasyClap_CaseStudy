using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]private float price;

    public float Price { get => price; set => price = value; }
}
