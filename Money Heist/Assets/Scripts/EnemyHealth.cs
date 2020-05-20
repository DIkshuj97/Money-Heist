using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 40f;
    public AudioClip enemyHitSFX;
    public AudioClip enemyDeathSFX;

    public void TakeDamage(float damage)
    {
        health -= damage;
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
        GetComponent<AIController>().Aggrevate();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
