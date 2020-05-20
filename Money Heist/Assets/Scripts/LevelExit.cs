using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            if (FindObjectOfType<ScenePersist>())
            {
                Destroy(FindObjectOfType<ScenePersist>().gameObject);
            }
            FindObjectOfType<Fader>().FadeToLevel();
        }
    }
}
