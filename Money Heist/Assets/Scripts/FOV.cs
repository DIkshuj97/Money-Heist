using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public bool playerInSight = false;

    private void Update()
    {
        if(!playerInSight)
        {
            GetComponent<SpriteRenderer>().color = new Color32(6, 253, 32, 61);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 61);
            playerInSight = true;
        }
    }
}
