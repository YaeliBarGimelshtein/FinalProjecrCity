using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public Rig handIk;
    public Transform weaponParent;
    RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullets(Time.deltaTime);

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                weapon.StopFiring();
            }
        }
        else
        {
            handIk.weight = 0.0f;
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        weapon = newWeapon;
        weapon.raycastDestenation = crossHairTarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        handIk.weight = 1.0f;
    }
}
