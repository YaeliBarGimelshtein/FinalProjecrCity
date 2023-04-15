using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindWeaponAction", menuName = "Ai/UtilityAI/Actions/FindWeaponAction")]
public class FindWeaponAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        agent.DoFindWeapon(3);
    }
}
