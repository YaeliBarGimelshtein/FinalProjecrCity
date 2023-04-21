using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindAmmoAction", menuName = "Ai/UtilityAI/Actions/FindAmmoAction")]
public class FindAmmoAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindAmmo");
        agent.DoFindAmmo(3);
    }
}