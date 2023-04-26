using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapons : MonoBehaviour
{
    public enum WeaponState
    {
        Holstered,
        Active,
        Reloading
    }
    public RaycastWeapon currentWeapon;
    private Animator animator;
    private MeshSockets sockets;
    private WeaponIk weaponIk;
    private Transform currentTarget;
    WeaponState weaponState = WeaponState.Holstered;

    public float inaccuracy = 0.0f;
    private GameObject magazineHand;

    public bool IsActive()
    {
        return weaponState == WeaponState.Active;
    }

    public bool IsHolstered()
    {
        return weaponState == WeaponState.Holstered;
    }

    public bool IsReloading()
    {
        return weaponState == WeaponState.Reloading;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIk = GetComponent<WeaponIk>();
    }

    private void Update()
    {
        if(currentTarget && currentWeapon && IsActive())
        {
            Vector3 target = currentTarget.position + weaponIk.targetOffset;
            target += Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateWeapon(Time.deltaTime, target);
        }
    }

    public void SetFiring(bool enabled)
    {
        if(enabled)
        {
            currentWeapon.StartFiring();
        }
        else
        {
            currentWeapon.StopFiring();
        }
    }

    public void Equip(RaycastWeapon weapon)
    {
        currentWeapon = weapon;
        sockets.Attach(weapon.transform, MeshSockets.SocketId.Spine);
    }

    public void ActivateWeapon()
    {
        StartCoroutine(EquipWeapon());
    }

    IEnumerator EquipWeapon()
    {
        animator.SetBool("Equip", true);
        yield return new WaitForSeconds(0.5f);
        while(animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }

        //when animation finishing playing
        if (currentWeapon)
        {
            weaponIk.SetAimTransform(currentWeapon.raycastOrigin);
            weaponState = WeaponState.Active;
        }
    }

    public void DeactivateWeapon()
    {
        SetTarget(null);
        SetFiring(false);
        StartCoroutine(HolsterWeapon());
    }

    public void ReloadWeapon()
    {
        if(IsActive())
        {
            StartCoroutine(ReloadWeaponCoroutine());
        }
    }

    IEnumerator HolsterWeapon()
    {
        weaponState = WeaponState.Holstered;
        animator.SetBool("Equip", false);
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }

        //when animation finishing playing
        if (currentWeapon)
        {
            weaponIk.SetAimTransform(currentWeapon.raycastOrigin);
        }
    }

    IEnumerator ReloadWeaponCoroutine()
    {
        weaponState = WeaponState.Reloading;
        animator.SetTrigger("reload_weapon");
        weaponIk.enabled = false;
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }
        if (currentWeapon)
        {
            //when animation finishing playing
            weaponIk.enabled = true;
            weaponState = WeaponState.Active;
        }
    }

    public void DropWeapon()
    {
        if(currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;
        }
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    public void OnAnimationEvent(string eventName)
    {
        if(eventName.Equals("equipWeapon"))
        {
            sockets.Attach(currentWeapon.transform, MeshSockets.SocketId.RightHand);
        }
        else if(eventName.Equals("holsterWeapon"))
        {
            sockets.Attach(currentWeapon.transform, MeshSockets.SocketId.Spine);
        }
    }

    public void SetTarget(Transform target)
    {
        weaponIk.SetTargetTransform(target);
        currentTarget = target;
    }

    void OnWeaponAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }

    void DetachMagazine()
    {
        var leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        RaycastWeapon weapon = currentWeapon;
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }

    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        droppedMagazine.transform.localScale = Vector3.one;
        magazineHand.SetActive(false);
    }

    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }

    void AttachMagazine()
    {
        RaycastWeapon weapon = currentWeapon;
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.RefillAmmo();
        animator.ResetTrigger("reload_weapon");
    }

    public void RefillAmmo(int clipCount)
    {
        var weapon = currentWeapon;
        if (weapon)
        {
            weapon.clipCount += clipCount;
        }
    }

    public bool IsLowAmmo()
    {
        var weapon = currentWeapon;
        if (weapon)
        {
            return weapon.IsLowAmmo();
        }
        return false;
    }
}
