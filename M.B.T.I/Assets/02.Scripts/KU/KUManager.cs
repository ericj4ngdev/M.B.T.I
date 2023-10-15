using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KUManager : MonoBehaviour
{
    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteKUChallenge();
    }
}
