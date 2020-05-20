using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    GameCanvas HealthCanvas;
    public AudioClip hitSFX;

    private void Start()
    {
        HealthCanvas = FindObjectOfType<GameCanvas>();
    }
    public void TakeDamage(float damage)
    {
        HealthCanvas.health -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitSFX);
        
        if (HealthCanvas.health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameStatus>().HandleLoseCondition();
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (HealthCanvas.health <= HealthCanvas.maxHealth)
        {
            HealthCanvas.health += amount;
        }
    }
}
