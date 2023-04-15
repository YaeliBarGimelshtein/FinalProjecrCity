using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindTargetAction", menuName = "Ai/UtilityAI/Actions/FindTargetAction")]
public class FindTargetAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("I just found Target");
        //Logic to find Target
        // Wander
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            agent.navMeshAgent.destination = worldBounds.RandomPosition();
        }
        //Decide our new best action after you finish this one
        agent.OnFinisherdAction();
    }
}