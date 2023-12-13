using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
   



    [Header("Attributes")]
    [SerializeField] private float turretRange = 3.5f;
    [SerializeField] private float TurretRotationSpeed = 5f;
    private Transform target;
    



    private void Update()
    {
        if (target == null)
        {
            findTarget();
        }

        RotateTowardsTarget();

        checkTargetIsInRange();
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void checkTargetIsInRange()
    {

        if (target == null)
        {
            return;
        }
        if (Vector2.Distance(target.position, transform.position) > turretRange)
        {
            target = null;
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


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, turretRange);

    }
}
