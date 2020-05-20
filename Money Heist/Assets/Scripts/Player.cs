using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   [SerializeField] float _speed = 5f;
   [SerializeField] Camera cam=null;
   [SerializeField] Transform firePoint=null;
   [SerializeField] GameObject bulletPrefab=null;
   [SerializeField] AudioClip outofAmmoSFX=null;
   [SerializeField] AudioClip shootSFX=null;

    Vector2 movement;
    Vector2 mousePos;
    Rigidbody2D myrigidbody;
    AudioSource myaudio;

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myaudio = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();   
    }

    private void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
        myrigidbody.MovePosition(myrigidbody.position + movement * _speed * Time.deltaTime);

        Vector2 lookDir = mousePos - myrigidbody.position;

        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg;
        myrigidbody.rotation = angle;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                myaudio.PlayOneShot(shootSFX, 0.3f);
            }

            else if (FindObjectOfType<GameCanvas>().Ammo > 0) 
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                myaudio.PlayOneShot(shootSFX,0.3f);
                FindObjectOfType<GameCanvas>().Ammo--;
            }

            else
            {
                myaudio.PlayOneShot(outofAmmoSFX);
            }

            
        }
    }
}
 