using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UtilityAiAgent : MonoBehaviour
{
    [HideInInspector] public AiBrain aiBrain;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public UtilityAiAction[] actionsAvailable;


    public AiAgentConfig config;
    

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public Ragdoll ragdoll;
    [HideInInspector] public SkinnedMeshRenderer mesh;
    [HideInInspector] public UiHealthBar healthBar;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public AiWeapons weapons;
    [HideInInspector] public AiSensor sensor;
    [HideInInspector] public AiTargetingSystem targetingSystem;
    [HideInInspector] public Health health;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aiBrain = GetComponent<AiBrain>();

        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UiHealthBar>();
        weapons = GetComponent<AiWeapons>();
        sensor = GetComponent<AiSensor>();
        targetingSystem = GetComponent<AiTargetingSystem>();
        health = GetComponent<Health>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiBrain.finishedDeciding)
        {
            aiBrain.finishedDeciding = false;
            aiBrain.BestAction.Execute(this);
        }
    }


    public void OnFinisherdAction()
    {
        aiBrain.DecideBestAction(actionsAvailable);
    }

  
}
