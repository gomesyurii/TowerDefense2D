using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Attributes")]
    [SerializeField] private float turretRange = 3.5f;
    [SerializeField] private float TurretRotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;
    [SerializeField] private int towerCost = 5;

    private Transform target;
    private float timeSinceLastShot = 0f;

    private void Update()
    {
        if (target == null)
        {
            findTarget();
        }

        RotateTowardsTarget();
        if (!checkTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= 1f / bulletsPerSecond)
            {
                Shoot(); 
                timeSinceLastShot = 0f;
            }
        }

    }
    
    private void Shoot() 
    {
        GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    public int GetCost()
    {
        return  towerCost;
    }



    private bool checkTargetIsInRange()
    {

        if (target == null)
        {
            return false;
        }

        if (Vector2.Distance(target.position, transform.position) > turretRange)
        {
            target = null;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null)
        {
            return;
        }

        float angle = Mathf.Atan2(target.position.y - turretRotationPoint.position.y, target.position.x - turretRotationPoint.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        transform.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, TurretRotationSpeed * Time.deltaTime);
    }


   // private void OnDrawGizmosSelected()
   // {
  //      Handles.color = Color.cyan;
  //      Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
   // }
}
