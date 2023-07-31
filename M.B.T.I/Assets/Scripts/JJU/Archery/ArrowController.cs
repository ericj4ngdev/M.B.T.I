using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint, arrowRotationPoint;

    [SerializeField]
    private float arrowMaxSpeed = 10;
    //private Transform midPointLocation;
    //private Quaternion midPointRotation;

    public void PrepareArrow()
    {
        Debug.Log("PrepareBowString");
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        Debug.Log("ResetBowString");
        midPointVisual.SetActive(false);

        Debug.Log($"Bow strength is {strength}");

        GameObject arrow = Instantiate(arrowPrefab);

       //Vector3 newRotation = new Vector3(arrowSpawnPoint.transform.rotation.x, 
       //                                  arrowSpawnPoint.transform.rotation.y -90,
       //                                 arrowSpawnPoint.transform.rotation.z);

        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowSpawnPoint.transform.rotation;
        Debug.Log(arrow.transform.rotation.eulerAngles);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(-midPointVisual.transform.right * strength * arrowMaxSpeed, ForceMode.Impulse);
    }
}
