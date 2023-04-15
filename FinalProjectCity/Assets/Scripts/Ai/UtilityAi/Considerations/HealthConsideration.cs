using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConsideration", menuName = "Ai/UtilityAI/Considerations/Health Consideration")]
public class HealthConsideration : UtilityAiConsideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration()
    {
        //Score = responseCurve.Evaluate(Mathf.Clamp01(UtilityAiAgent.))
        return 0.5f;
    }
}
