using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * offset.y
                                             + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
                                             + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ����ϱ� 
        if (other.CompareTag("Left Hand"))
        {
            Debug.Log("left Hand");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Left Hand"))
        {
            Debug.Log("left Hand");
        }
        // grip�ϸ� �ȴ�. 
    }
}
