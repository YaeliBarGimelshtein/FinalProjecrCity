using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain : MonoBehaviour
{
    public UtilityAiAction BestAction { get; set; }

    private UtilityAiAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UtilityAiAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecideBestAction(UtilityAiAction[] actionsAvailabe)
    {

    }

    public void ScoreAction(UtilityAiAction action)
    {

    }
}
