using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int clipAmount = 2;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            activeWeapon.RefillAmmo(clipAmount);
        }

        //AiWeapons aiWeapons = other.gameObject.GetComponent<AiWeapons>();
        //if (aiWeapons)
        //{
            
        //}
    }
}
