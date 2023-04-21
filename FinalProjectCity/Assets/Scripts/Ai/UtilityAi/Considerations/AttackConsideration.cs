using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackConsideration", menuName = "Ai/UtilityAI/Considerations/Attack Consideration")]
public class AttackConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if (!agent.weapons.HasWeapon() || !agent.targetingSystem.HasTarget)
        {
            return 0f;
        }

        return 0.8f;
    }
}
