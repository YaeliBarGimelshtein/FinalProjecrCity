using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChaseConsideration", menuName = "Ai/UtilityAI/Considerations/Chase Consideration")]

public class ChaseConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
