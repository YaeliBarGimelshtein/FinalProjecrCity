using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FindHealthAction", menuName = "Ai/UtilityAI/Actions/FindHealthAction")]
public class FindHealthAction : UtilityAiAction
{

    public override void Execute(UtilityAiAgent agent)
    {
        agent.DoFindHealth(3);
    }
}
