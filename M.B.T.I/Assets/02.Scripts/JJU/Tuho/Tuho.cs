using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuho : MonoBehaviour
{
    private float strength;
    private float tuhoMaxSpeed = 8;
    private Vector3 previousControllerPosition;

    public void OnSelectEntered()
    {
        strength = 0;

    }

    public void OnSelectExited()
    {

    }


    void Start()
    {
        previousControllerPosition = transform.position;
    }

    void Update()
    {
    }

    public void ReleaseTuho()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * 2 * tuhoMaxSpeed, ForceMode.Impulse);
    }

}
