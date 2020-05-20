using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] PatrolPath patrolPath=null;
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float attackSpeedFraction = 0.5f;
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float waypointTolerance = 0.5f;
    [SerializeField] float waypointDwellTime = 3f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float attackDistance = 0.5f;
    [SerializeField] float agroCooldownTime = 3f;
    [SerializeField] float timeBetweenShots = 0.5f;

    [Header("Bullet Objects")]
    [SerializeField] Transform firePoint=null;
    [SerializeField] GameObject bulletPrefab=null;
    [SerializeField] AudioClip shootSFX=null;

    int currentWaypointIndex = 0;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceAggrevated = Mathf.Infinity;
    bool canShoot = true;

    Rigidbody2D rb;
    public Player player;
    FOV enemyFOV;
    AudioSource aiaudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        aiaudio = GetComponent<AudioSource>();
        enemyFOV = GetComponentInChildren<FOV>();
    }

    void Update()
    {
       if(player==null)
        {
            player= FindObjectOfType<Player>();
        }
        if (IsAggrevated())
        { 
           AttackBehaviour();
        }

        else if(timeSinceLastSawPlayer<suspicionTime)
        {
            SuspiciousBehaviour();
        }

        else
        {
           PatrolBehaviour();
        }
        Updatetimers();
    }

    void Updatetimers()
    {
        timeSinceAggrevated += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    public void Aggrevate()
    {
        timeSinceAggrevated = 0;
    }

    private void SuspiciousBehaviour()
    {
        //Do Nothing
    }

    private void AttackBehaviour()
    {
        timeSinceLastSawPlayer = 0;
        if(player==null)
        {
            enemyFOV.playerInSight = false;
            return;
        }

        AIRotation(player.GetComponent<Rigidbody2D>().position);
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if(distanceToPlayer>attackDistance && distanceToPlayer<chaseDistance )
        {
            ChasePlayer();
        }

        else if(distanceToPlayer>chaseDistance)
        {
            enemyFOV.playerInSight = false;
        }

        else
        {
            StopAndAttack();
        }
    }

    private void StopAndAttack()
    {
        if (canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        aiaudio.PlayOneShot(shootSFX,0.2f);
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attackSpeedFraction * Time.deltaTime);
    }

    private void PatrolBehaviour()
    {
        Vector2 nextPosition = transform.position;

        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWayPoint();
            }
            nextPosition = GetCurrentWaypoint();
            AIRotation(nextPosition);
        }
        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, patrolSpeedFraction * Time.deltaTime);
        }
    }

    private Vector2 GetCurrentWaypoint()
    {
        return patrolPath.GetWayPoint(currentWaypointIndex);
    }

    private void CycleWayPoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWayPoint()
    {
        float distanceToWaypoint = Vector2.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private bool IsAggrevated()
    {
       return enemyFOV.playerInSight || timeSinceAggrevated < agroCooldownTime;
    }

    private void AIRotation(Vector2 position)
    {
        Vector2 lookDir = position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.cyan;
         Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
