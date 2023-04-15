using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindTargetConsideration", menuName = "Ai/UtilityAI/Considerations/Find Target Consideration")]

public class FindTargetConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
