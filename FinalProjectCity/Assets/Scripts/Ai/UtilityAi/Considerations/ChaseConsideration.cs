using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChaseConsideration", menuName = "Ai/UtilityAI/Considerations/Chase Consideration")]

public class ChaseConsideration : UtilityAiConsideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(UtilityAiAgent agent)
    {

        //Score = responseCurve.Evaluate(Mathf.Clamp01(agent.he / agent.health.maxHealth));
        return 0.1f;
    }
}
