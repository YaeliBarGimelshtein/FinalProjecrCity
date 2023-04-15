using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackTargetAction", menuName = "Ai/UtilityAI/Actions/AttackTargetAction")]
public class AttackTargetAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("I just attacked target");
        //Logic to attacked target

        if (agent.targetingSystem.HasTarget)
        {
            agent.weapons.SetTarget(agent.targetingSystem.Target.transform);
            agent.navMeshAgent.destination = agent.targetingSystem.TargetPosition;

            ReloadWeapon(agent);
            UpdateFiring(agent);
        }

        //Decide our new best action after you finish this one
        agent.OnFinisherdAction();
    }

    private void ReloadWeapon(UtilityAiAgent agent)
    {
        var weapon = agent.weapons.currentWeapon;
        if (weapon && weapon.ShouldReload())
        {
            agent.weapons.ReloadWeapon();
        }
    }
    private void UpdateFiring(UtilityAiAgent agent)
    {
        if (agent.targetingSystem.TargetInSight)
        {
            agent.weapons.SetFiring(true);
        }
        else
        {
            agent.weapons.SetFiring(false);
        }
    }
}
