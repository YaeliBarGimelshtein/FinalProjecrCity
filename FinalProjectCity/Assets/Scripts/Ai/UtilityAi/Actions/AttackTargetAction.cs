using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackTargetAction", menuName = "Ai/UtilityAI/Actions/AttackTargetAction")]
public class AttackTargetAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        agent.DoAttackTarget(3);
    }
}
