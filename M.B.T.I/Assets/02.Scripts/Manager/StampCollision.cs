using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampCollision : MonoBehaviour
{
    [SerializeField]
    GameObject[] stampUI = new GameObject[7];
    int[] stampActive = { 0, 0, 0, 0, 0, 0, 0 };


    public void OnCollisionEnter(Collision collision)
    {
        int sum = 0;

        Debug.Log("충돌");
        if (collision.gameObject.name == "stamp_kmu")
        {
            Debug.Log("이건 계명대야");
            stampUI[0].SetActive(true);
            stampActive[0] = 1;

        }
        else if (collision.gameObject.name == "stamp_ku")
        {
            stampUI[1].SetActive(true);
            stampActive[1] = 1;
        }
        else if (collision.gameObject.name == "stamp_khu")
        {
            stampUI[2].SetActive(true);
            stampActive[2] = 1;
        }
        else if (collision.gameObject.name == "stamp_pcu")
        {
            stampUI[3].SetActive(true);
            stampActive[3] = 1;
        }
        else if (collision.gameObject.name == "stamp_chu")
        {
            stampUI[4].SetActive(true);
            stampActive[4] = 1;
        }
        else if (collision.gameObject.name == "stamp_kwu")
        {
            stampUI[5].SetActive(true);
            stampActive[5] = 1;
        }
        else if (collision.gameObject.name == "stamp_jju")
        {
            stampUI[6].SetActive(true);
            stampActive[6] = 1;
        }
        // 스탬프 완성
        foreach (int i in stampActive)
        {
            sum += i;
        }

        if (sum == 7)
        {
            Debug.Log("스탬프 완성");
        }
    }

}
