using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20;
    public float speed = 20;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right *speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
           
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, 1f);
}
}
