using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapons : MonoBehaviour
{
    RaycastWeapon currentWeapon;

    public void Equip(RaycastWeapon weapon)
    {
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(transform, false);
    }
}
