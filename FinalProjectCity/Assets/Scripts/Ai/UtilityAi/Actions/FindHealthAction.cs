using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindHealthAction", menuName = "Ai/UtilityAI/Actions/FindHealthAction")]
public class FindHealthAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindHealth");
        if (!agent.weapons.IsHolstered())
        {
            agent.weapons.DeactivateWeapon();
            agent.navMeshAgent.stoppingDistance = 0.0f;
            agent.navMeshAgent.speed = agent.config.findHealthSpeed;
        }
        agent.DoFindHealth(3);
    }
}
