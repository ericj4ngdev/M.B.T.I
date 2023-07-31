using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ����ϱ� 
        if (other.CompareTag("Left Hand"))
        {
            //Debug.Log("left Hand");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Left Hand"))
        {
            //Debug.Log("left Hand");
        }
        // grip�ϸ� �ȴ�. 
    }
}
