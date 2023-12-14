using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    private GameObject tower;

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private Color originalColor;

    void Start()
    {
        originalColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = originalColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        GameObject towerPrefab = BuildManager.main.GetTowerPrefab();
        Turret towerScript = towerPrefab.GetComponent<Turret>();

        if (LevelManager.main.AvailableTurrets < 1) return; 

        tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        LevelManager.main.DecreaseCurrency(towerScript.GetCost());
        LevelManager.main.AvailableTurrets -= 1;
    }
}
