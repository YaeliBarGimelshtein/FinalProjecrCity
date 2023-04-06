using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private Ragdoll ragdoll;
    private ActiveWeapon weapons;
    private CharacterAiming aiming;

    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        weapons = GetComponent<ActiveWeapon>();
        aiming = GetComponent<CharacterAiming>();
    }

    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActivateRagdoll();
        weapons.DropWeapon();
        aiming.enabled = false;
    }

    protected override void OnDamage(Vector3 direction)
    {

    }
}
