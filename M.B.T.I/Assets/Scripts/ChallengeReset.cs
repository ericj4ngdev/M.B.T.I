using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPref 초기화
        if (PlayerPrefs.HasKey("Challenges"))
        {
            PlayerPrefs.DeleteKey("Challenges");
        }
    }
}
