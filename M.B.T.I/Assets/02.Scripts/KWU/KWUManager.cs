using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KWUManager : MonoBehaviour
{
    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteKWUChallenge();
    }
}
