using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeathConsideration", menuName = "Ai/UtilityAI/Considerations/Death Consideration")]

public class DeathConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
