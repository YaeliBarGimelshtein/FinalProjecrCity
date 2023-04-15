using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeathAction", menuName = "Ai/UtilityAI/Actions/DeathAction")]
public class DeathAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("I just died");
        //Logic die
    }
}
