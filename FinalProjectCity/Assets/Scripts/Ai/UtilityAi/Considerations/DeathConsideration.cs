using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeathConsideration", menuName = "Ai/UtilityAI/Considerations/Death Consideration")]

public class DeathConsideration : UtilityAiConsideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if(agent.health.currentHealth == 0)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }
}

