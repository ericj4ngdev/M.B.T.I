using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField]
    private GameObject lantern;
    private float spawnTransformX;
    private float spawnTransformZ;
    private float randomYRotation;

    void Start()
    {
        lantern.transform.parent = null;

        lantern.transform.position = new Vector3(0, 0, 0);

        randomYRotation = Random.Range(0f, 360f);

        lantern.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);
        Destroy(lantern, 10f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        Destroy(lantern);
    }
}
