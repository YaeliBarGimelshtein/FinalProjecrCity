using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConsideration", menuName = "Ai/UtilityAI/Considerations/Health Consideration")]
public class HealthConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if(agent.health.currentHealth <= 40)
        {
            return 0.9f;
        }
        
        return 0f;
    }
}
