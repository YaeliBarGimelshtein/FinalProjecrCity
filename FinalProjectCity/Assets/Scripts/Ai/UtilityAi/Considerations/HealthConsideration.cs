using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConsideration", menuName = "Ai/UtilityAI/Considerations/Health Consideration")]
public class HealthConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.5f;
    }
}
