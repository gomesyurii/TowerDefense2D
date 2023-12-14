using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    private GameObject tower;
    public int currency;
    public Transform startPoint;
    public int AvailableTurrets;
    public Turret towerPrefab;
    public Transform[] path;

    public void Awake()
    {

        main = this;
        currency = 10;
        AvailableTurrets = 2;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public void DecreaseCurrency(int amount)
    {
        currency -= amount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buyTurret();
        }
    }

    public void buyTurret()
    {
         Turret towerScript = towerPrefab.GetComponent<Turret>();
        if (currency >= towerScript.GetCost())
        {
            AvailableTurrets += 1;
            currency -= towerScript.GetCost();
        }
    }


}
