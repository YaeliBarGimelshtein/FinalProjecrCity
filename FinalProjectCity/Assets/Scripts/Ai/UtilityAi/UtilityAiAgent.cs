using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UtilityAiAgent : MonoBehaviour
{
    [HideInInspector] public AiBrain aiBrain;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public UtilityAiAction[] actionsAvailable;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aiBrain = GetComponent<AiBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
