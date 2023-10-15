using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnalbeObject : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform respawnTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("on collision");
        if (collision.gameObject.tag == "Ground")
        {

            Debug.Log("땅에 떨어짐!");
            //Invoke("RespawnObject", 1f);
        }
    }

    private void RespawnObject()
    {
        this.gameObject.SetActive(false);
        Instantiate(prefab, respawnTransform);
    }
}
