using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapons : MonoBehaviour
{
    private RaycastWeapon currentWeapon;
    private Animator animator;
    private MeshSockets sockets;
    private WeaponIk weaponIk;
    private Transform currentTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIk = GetComponent<WeaponIk>();
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
        weaponIk.SetAimTransform(currentWeapon.raycastOrigin);

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
    }

    public void SetTarget(Transform target)
    {
        weaponIk.SetTargetTransform(target);
        currentTarget = target;
    }
}
