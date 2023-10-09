using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject midPointVisual, arrowPrefab, arrowSpawnPoint, bowPrefab;



    [SerializeField]
    private float arrowMaxSpeed = 10;

    public void PrepareArrow()
    {
        Debug.Log("PrepareArrow");
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        Debug.Log("ReleaseArrow");
        midPointVisual.SetActive(false);

        GameObject arrow = Instantiate(arrowPrefab);

        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowSpawnPoint.transform.rotation;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddRelativeForce(-rb.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
        //rb.AddForce(-midPointVisual.transform.right * strength * arrowMaxSpeed, ForceMode.Impulse);


    }

}
