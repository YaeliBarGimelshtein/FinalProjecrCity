using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateId initialState;
    public AiAgentConfig config;

    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
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
        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UiHealthBar>();
        weapons = GetComponent<AiWeapons>();
        sensor = GetComponent<AiSensor>();
        targetingSystem = GetComponent<AiTargetingSystem>();
        health = GetComponent<Health>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiFindWeaponState());
        stateMachine.RegisterState(new AiAttackTargetState());
        stateMachine.RegisterState(new AiFindTargetState());
        stateMachine.RegisterState(new AiFindHealthState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
