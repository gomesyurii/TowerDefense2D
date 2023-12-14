using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int selectedTower = 0;

    private void Awake()
    {
        if (main != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }

        main = this;
    }

    public GameObject GetTowerPrefab()
    {
        return towerPrefabs[selectedTower];
    }

    








}
