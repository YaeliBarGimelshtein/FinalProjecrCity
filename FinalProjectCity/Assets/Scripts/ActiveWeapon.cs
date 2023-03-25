using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public Rig handIk;
    public Transform weaponParent;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;
    public Animator rigController;

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
            if (Input.GetKeyDown(KeyCode.Mouse1) && !GetIsHolstered())
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

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                bool isHolstered = GetIsHolstered();
                rigController.SetBool("holster_weapon", !isHolstered);
            }
        }
        
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestenation = crossHairTarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + weapon.weaponName);
    }
    
    private bool GetIsHolstered()
    {
        return rigController.GetBool("holster_weapon");
    }
}
