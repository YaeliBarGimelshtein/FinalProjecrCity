using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindAmmoAction", menuName = "Ai/UtilityAI/Actions/FindAmmoAction")]
public class FindAmmoAction : UtilityAiAction
{
    GameObject pickup;
    GameObject[] pickups = new GameObject[1];

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("I just found ammo");
        //Logic to find ammo
        // Find pickup
        if (!pickup)
        {
            pickup = FindPickup(agent, "Ammo");

            if (pickup)
            {
                CollectPickup(agent, pickup);
            }
        }

        // Wander
        if (!agent.navMeshAgent.hasPath && !pickup)// added !pickup to fix soldier not taking gun if it is infront of him at the start of the game
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            agent.navMeshAgent.destination = worldBounds.RandomPosition();
        }

        //Decide our new best action after you finish this one
        agent.OnFinisherdAction();
    }

    GameObject FindPickup(UtilityAiAgent agent, string filter)
    {
        int count = agent.sensor.Filter(pickups, "Pickup", filter);
        if (count > 0)
        {
            return pickups[0];
        }
        return null;
    }

    void CollectPickup(UtilityAiAgent agent, GameObject pickup)
    {
        agent.navMeshAgent.destination = pickup.transform.position;
    }
}