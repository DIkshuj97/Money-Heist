using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AudioClip ammoSFX;
    public int ammoAmount = 5;

    bool pickedUp=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
        {
            if (other.gameObject.tag == "Player")
            {
                pickedUp = true;
                FindObjectOfType<GameCanvas>().IncreaseCurrentAmmo(ammoAmount);
                AudioSource.PlayClipAtPoint(ammoSFX, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
