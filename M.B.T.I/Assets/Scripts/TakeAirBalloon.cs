using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAirBalloon : MonoBehaviour
{
    public GameObject airballoon;
    public GameObject player;

    [SerializeField]
    private int yPosition = 0;

    //private bool isOnPlane = false;

    private void Update()
    {
        if (airballoon.GetComponent<AirBalloon>().isPlayerWantToGetOff)
        {
            if (airballoon.transform.position == airballoon.GetComponent<AirBalloon>().GetNextStop().transform.position)
            {
                Invoke("GetOff", 0.5f);
            }
        }
    }

    public void OnClickAirballoon()
    {
        // 열기구 올라타기
        //Vector3 newPosition = new Vector3(airballoon.GetComponent<AirBalloon>().GetTransform().position.x, airballoon.GetComponent<AirBalloon>().GetTransform().position.y + yPosition, airballoon.GetComponent<AirBalloon>().GetTransform().position.z);

        player.transform.position = airballoon.transform.position;
        transform.SetParent(airballoon.transform);
    }

    public void OnClickExit()
    {
        airballoon.GetComponent<AirBalloon>().isPlayerWantToGetOff = true;
        airballoon.GetComponent<AirBalloon>().StopToGetOff();
    }

    private void GetOff()
    {
        transform.SetParent(null);
        // 열기구 내리기
        Vector3 currentPosition = player.transform.position;

        currentPosition.y = 5f;

        player.transform.position = currentPosition;
        airballoon.GetComponent<AirBalloon>().isPlayerWantToGetOff = false;
    }
}
