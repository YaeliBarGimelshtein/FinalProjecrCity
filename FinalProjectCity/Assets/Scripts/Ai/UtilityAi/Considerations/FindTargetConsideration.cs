using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindTargetConsideration", menuName = "Ai/UtilityAI/Considerations/Find Target Consideration")]

public class FindTargetConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if(agent.weapons.HasWeapon())
        {
            return 0.75f;
        }
        //Score = responseCurve.Evaluate(Mathf.Clamp01(agent.he / agent.health.maxHealth));
        return 0f;
    }
}

