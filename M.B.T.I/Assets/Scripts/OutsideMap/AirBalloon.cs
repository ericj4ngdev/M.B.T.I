using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloon : MonoBehaviour
{
    private float currentSpeed = 0;
    [SerializeField]
    private float moveSpeed = 0.2f;
    //private Vector3 currentLocation;
    //private Vector3 startLocation;

    public bool isPlayerOnBoard = false;

    // Start is called before the first frame update
    void Start()
    {
     //   InitAirballoon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Vector3 GetAirBalloonLocation()
    //{
    //    return currentLocation;
    //}

    public void Stop()
    {
        currentSpeed = 0;
    }

    private void Move()
    {
        currentSpeed = moveSpeed;
    }

    //void InitAirballoon()
    //{
    //    currentLocation = startLocation;
    //}

}
