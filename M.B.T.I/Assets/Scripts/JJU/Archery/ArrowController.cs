using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint, arrowRotationPoint, bowPrefab;
    [SerializeField]
    private Transform respawnTransform;



    [SerializeField]
    private float arrowMaxSpeed = 10;
    private int arrowCount = 0;
    private int arrowMaxCount = 5;

    private GameObject[] arrows = new GameObject[5];

    public void Start()
    {
        arrowCount = 0;
    }

    private void Update()
    {
        if (arrowCount == arrowMaxCount)
            Invoke("RespawnObject", 1f);
    }

    public void PrepareArrow()
    {
        //Debug.Log("PrepareBowString");
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        
        midPointVisual.SetActive(false);

        GameObject arrow = Instantiate(arrowPrefab);
        arrows[arrowCount] = arrow;

        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowSpawnPoint.transform.rotation;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(-midPointVisual.transform.right * strength * arrowMaxSpeed, ForceMode.Impulse);


        arrowCount++;

        //if (arrowCount + 1 == arrowMaxCount)
        //{
        //    Invoke("RespawnObject", 1f);
        //    return;
        //}
    }

    private void RespawnObject()
    {
        GameObject newObject = Instantiate(bowPrefab, respawnTransform);
        foreach (GameObject obj in arrows)
        {
            if (obj.activeInHierarchy)
                Destroy(obj);
        }
        Destroy(this.gameObject);
    }
}
