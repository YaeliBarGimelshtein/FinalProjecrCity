using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmoConsideration", menuName = "Ai/UtilityAI/Considerations/Ammo Consideration")]

public class AmmoConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.9f;
    }
}
