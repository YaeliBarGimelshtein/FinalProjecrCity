using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : Health
{
    private Ragdoll ragdoll;
    private ActiveWeapon weapons;
    private CharacterAiming aiming;
    private VolumeProfile postProcessing;
    private CameraManager cameraManager;

    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        weapons = GetComponent<ActiveWeapon>();
        aiming = GetComponent<CharacterAiming>();
        postProcessing = FindObjectOfType<Volume>().profile;
        cameraManager = FindObjectOfType<CameraManager>();
    }

    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActivateRagdoll();
        direction.y = 1.0f;
        ragdoll.ApplyForce(direction);
        weapons.DropWeapon();
        aiming.enabled = false;
        cameraManager.EnableKillCam();
    }

    protected override void OnDamage(Vector3 direction)
    {
        UpdateVignette();
    }

    protected override void OnHeal(float amount)
    {
        UpdateVignette();
    }

    private void UpdateVignette()
    {
        Vignette vignette;
        if (postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.8f;
        }
    }
}
