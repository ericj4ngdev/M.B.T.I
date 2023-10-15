using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : MonoBehaviour
{
    public bool active;

    public void ActivateSignPost(GameObject signPost)
    {
        // 활성화되어 있는지 확인하고 끄기
        active = signPost.activeSelf;
        
        if (active == false)
        {
            active = true;
            signPost.SetActive(active);
        }
        else
        {
            active = false;
            signPost.SetActive(active);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
