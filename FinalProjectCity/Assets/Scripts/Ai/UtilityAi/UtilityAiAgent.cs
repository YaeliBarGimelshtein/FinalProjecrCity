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
    public GameObject pickup;
    GameObject[] pickups = new GameObject[1];

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

        pickup = null;
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

    public GameObject FindPickup(string filter)
    {
        int count = sensor.Filter(pickups, "Pickup", filter);
        if (count > 0)
        {
            return pickups[0];
        }
        return null;
    }

    public void CollectPickup(GameObject pickup)
    {
        navMeshAgent.destination = pickup.transform.position;
    }

    


    #region Coroutine



    public void DoFindHealth(int time)
    {
        StartCoroutine(FindHealthCoroutine(time));
    }
    IEnumerator FindHealthCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        //Logic to find Health
        // Find pickup
        if (!pickup)
        {
            pickup = FindPickup("Health");

            if (pickup)
            {
                CollectPickup(pickup);
            }
        }

        // Wander
        if (!navMeshAgent.hasPath && !pickup)// added !pickup to fix soldier not taking gun if it is infront of him at the start of the game
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            navMeshAgent.destination = worldBounds.RandomPosition();
        }
        
        //Decide our new best action after you finish this one
        OnFinisherdAction();
    }




    public void DoFindTarget(int time)
    {
        StartCoroutine(FindTargetCoroutine(time));
    }
    IEnumerator FindTargetCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        //Logic to find Target
        // Wander
        if (!navMeshAgent.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            navMeshAgent.destination = worldBounds.RandomPosition();
        }
        //Decide our new best action after you finish this one
        OnFinisherdAction();
    }




    public void DoFindWeapon(int time)
    {
        StartCoroutine(FindWeaponCoroutine(time));
    }
    IEnumerator FindWeaponCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        //Logic to find Weapon
        // Find pickup
        if (!pickup)
        {
            pickup = FindPickup("PickUpRifle");

            if (pickup)
            {
                CollectPickup(pickup);
            }
        }

        // Wander
        if (!navMeshAgent.hasPath && !pickup)// added !pickup to fix soldier not taking gun if it is infront of him at the start of the game
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            navMeshAgent.destination = worldBounds.RandomPosition();
        }

        
        //Decide our new best action after you finish this one
        OnFinisherdAction();
    }
    
    

    #endregion


}
