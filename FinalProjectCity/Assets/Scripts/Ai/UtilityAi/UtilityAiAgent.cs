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
    GameObject pickup;
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

    GameObject FindPickup(string filter)
    {
        int count = sensor.Filter(pickups, "Pickup", filter);
        if (count > 0)
        {
            return pickups[0];
        }
        return null;
    }

    void CollectPickup(GameObject pickup)
    {
        navMeshAgent.destination = pickup.transform.position;
    }

    
    private void ReloadWeapon()
    {
        var weapon = weapons.currentWeapon;
        if (weapon && weapon.ShouldReload())
        {
            weapons.ReloadWeapon();
        }
    }
    private void UpdateFiring()
    {
        if (targetingSystem.TargetInSight)
        {
            weapons.SetFiring(true);
        }
        else
        {
            weapons.SetFiring(false);
        }
    }


    #region Coroutine

    public void DoAttackTarget(int time)
    {
        StartCoroutine(AttackTargetCoroutine(time));

    }
    IEnumerator AttackTargetCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        Debug.Log("I just attacked target");
        //Logic to attacked target

        if (targetingSystem.HasTarget)
        {
            weapons.SetTarget(targetingSystem.Target.transform);
            navMeshAgent.destination = targetingSystem.TargetPosition;

            ReloadWeapon();
            UpdateFiring();
        }
        
        //Decide our new best action after you finish this one
        OnFinisherdAction();
    }



    public void DoFindAmmo(int time)
    {
        StartCoroutine(FindAmmoCoroutine(time));

    }
    IEnumerator FindAmmoCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        Debug.Log("I just found ammo");
        //Logic to find ammo
        // Find pickup
        if (!pickup)
        {
            pickup = FindPickup("Ammo");

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
        Debug.Log("I just found Health");
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
        Debug.Log("I just found Target");
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
        Debug.Log("I just found Weapon");
        //Logic to find ammo
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
