using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindTargetAction", menuName = "Ai/UtilityAI/Actions/FindTargetAction")]
public class FindTargetAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindTarget");
        agent.DoFindTarget(3);
    }
}