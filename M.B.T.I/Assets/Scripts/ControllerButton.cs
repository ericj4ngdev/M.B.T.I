using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerButton : MonoBehaviour
{
    public GameObject airballoon;
    public GameObject BallPrefab;
    public Transform XROrigin; 
    public Transform spawnspot;
    public InputActionProperty PressX;
    public InputActionProperty PressY;
    public InputActionProperty PressA;
    public InputActionProperty PressB;

    void SpawnBowlingBall()
    {
        GameObject Ball = Instantiate(BallPrefab);
        Ball.transform.position = spawnspot.position;
    }

    void Update()
    {
        if (PressX.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed X");
            // PinManager.Instance.SetPinPosition();
        }

        if (PressY.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed Y");
            // 열기구 올라타기
            Vector3 newPosition = new Vector3(airballoon.GetComponent<AirBalloon>().GetTransform().position.x, airballoon.GetComponent<AirBalloon>().GetTransform().position.y + 1, airballoon.GetComponent<AirBalloon>().GetTransform().position.z);

            XROrigin.transform.position = newPosition;
        }

        if (PressA.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed A");
            SpawnBowlingBall();
        }

        if (PressB.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed B");
            
        }
    }
}
