using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeathAction", menuName = "Ai/UtilityAI/Actions/DeathAction")]
public class DeathAction : UtilityAiAction
{
    public Vector3 direction;

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("I just died");
        //Logic die
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        agent.healthBar.gameObject.SetActive(false);
        agent.mesh.updateWhenOffscreen = true;
        agent.weapons.DropWeapon();
        agent.navMeshAgent.enabled = false;
    }
}
