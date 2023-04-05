using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        AiWeapons aiWeapons = other.gameObject.GetComponent<AiWeapons>();
        if (aiWeapons)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            aiWeapons.Equip(newWeapon);
            Destroy(gameObject);
        }
    }
}
