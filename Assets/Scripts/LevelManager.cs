using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public int currency;

    public Transform startPoint;
    public Transform[] path;
    public void Awake()
    {
        main = this;
        currency = 10;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public void DecreaseCurrency(int amount)
    {
        currency -= amount;
    }

}
