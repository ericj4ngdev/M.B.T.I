using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject newObject = Instantiate(robot);
    }

    public void Fail()
    {
        Invoke("RespawnRobot", 1f);

    }

    private void RespawnRobot()
    {
        GameObject newObject = Instantiate(robot);

        Destroy(robot);
    }
}
