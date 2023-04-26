using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindTargetAction", menuName = "Ai/UtilityAI/Actions/FindTargetAction")]
public class FindTargetAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindTarget");
        if (!agent.weapons.IsHolstered())
        {
            agent.weapons.DeactivateWeapon();
            agent.navMeshAgent.stoppingDistance = 0.0f;
            agent.navMeshAgent.speed = agent.config.findTargetSpeed;
        }
        agent.DoFindTarget(3);
    }
}