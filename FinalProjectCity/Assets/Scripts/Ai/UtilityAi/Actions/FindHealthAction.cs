using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindHealthAction", menuName = "Ai/UtilityAI/Actions/FindHealthAction")]
public class FindHealthAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        Debug.Log("In  FindHealth");
        agent.DoFindHealth(3);
    }
}
