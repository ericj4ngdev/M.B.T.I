using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject robotPrefab;
    private GameObject robot;
    [SerializeField]
    private Transform respawnedTransform;

    // Start is called before the first frame update
    void Start()
    { 
        //robot = Instantiate(robotPrefab, respawnedTransform.position, Quaternion.identity, null);
    }

    public void Fail()
    {
        Debug.Log("실패");

    }
}
