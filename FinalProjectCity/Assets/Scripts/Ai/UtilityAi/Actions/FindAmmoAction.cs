using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindAmmoAction", menuName = "Ai/UtilityAI/Actions/FindAmmoAction")]
public class FindAmmoAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindAmmo");
        
        if (!agent.weapons.IsHolstered())
        {
            agent.weapons.DeactivateWeapon();
            agent.navMeshAgent.stoppingDistance = 0.0f;
            agent.navMeshAgent.speed = agent.config.findTargetSpeed;
        }
        //Logic to find ammo
        // Find pickup
        if (agent.pickup == null || !agent.pickup)
        {
            agent.pickup = agent.FindPickup("Ammo");

            if (agent.pickup)
            {
                agent.CollectPickup(agent.pickup);
            }
        }

        // Wander
        if (!agent.navMeshAgent.hasPath && !agent.pickup)// added !pickup to fix soldier not taking gun if it is infront of him at the start of the game
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            agent.navMeshAgent.destination = worldBounds.RandomPosition();
        }

        //Decide our new best action after you finish this one
        agent.OnFinisherdAction();
    }
}