using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;



    private Transform target;
    private int pathIndex = 0;





    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }


    private void Update()
    {
        if (LevelManager.main.lives > 0 )
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                pathIndex++;



                if (pathIndex >= LevelManager.main.path.Length)
                {

                    EnemySpawner.OnEnemyDestroy.Invoke();
                    LevelManager.main.DecreaseLives(1);

                    Destroy(gameObject);
                    return;
                }
                else
                {
                    target = LevelManager.main.path[pathIndex];
                }
            }
        } else
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }




}
