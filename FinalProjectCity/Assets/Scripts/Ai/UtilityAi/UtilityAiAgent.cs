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
        //Logic to find ammo

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

        //Decide our new best action after you finish this one
        OnFinisherdAction();
    }
    
    

    #endregion


}
