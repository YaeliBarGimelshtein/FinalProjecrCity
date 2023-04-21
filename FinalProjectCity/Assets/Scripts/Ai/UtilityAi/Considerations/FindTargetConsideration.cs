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
        return 0f;
    }
}

