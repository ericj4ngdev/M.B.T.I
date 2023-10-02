using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JJUManager : MonoBehaviour
{
   public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteJJUChallenge();
    }
}
