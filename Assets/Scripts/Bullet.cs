using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private Rigidbody2D rb;



    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 5f;

    private Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }


        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }


}
