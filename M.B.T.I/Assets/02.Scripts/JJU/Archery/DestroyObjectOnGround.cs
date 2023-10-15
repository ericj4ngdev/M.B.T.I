using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnGround : MonoBehaviour
{
    private SphereCollider myCollider;

    private void Start()
    {
        myCollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("땅에 닿음");
            myCollider.enabled = false;
            Invoke("DestroyObject", 0.3f);
        }
    }

    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }

}
