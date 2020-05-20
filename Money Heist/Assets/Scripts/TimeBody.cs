using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;
    public float recordTime = 5f;

   public List<PointInTime>  pointsInTimes;

    private void Start()
    {
        pointsInTimes = new List<PointInTime>();
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Question))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Question))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
            GetComponentInChildren<FOV>().playerInSight = false;
        }
        else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if (pointsInTimes.Count > 0)
        {
            PointInTime pointInTime = pointsInTimes[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTimes.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if(pointsInTimes.Count>Mathf.Round(recordTime/Time.fixedDeltaTime))
        {
            pointsInTimes.RemoveAt(pointsInTimes.Count - 1);
        }
        pointsInTimes.Insert(0, new PointInTime(transform.position,transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
