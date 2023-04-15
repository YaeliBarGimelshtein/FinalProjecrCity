using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackConsideration", menuName = "Ai/UtilityAI/Considerations/Attack Consideration")]
public class AttackConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
