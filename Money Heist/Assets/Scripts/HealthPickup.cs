using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public AudioClip healthPickupSFX;
    public float healthAmount = 10f;

    bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
        {
            if (other.gameObject.tag == "Player")
            {
                pickedUp = true;
                other.GetComponent<Health>().IncreaseHealth(healthAmount);
                AudioSource.PlayClipAtPoint(healthPickupSFX, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
