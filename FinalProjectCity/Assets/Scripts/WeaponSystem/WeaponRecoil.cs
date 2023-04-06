using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public CharacterAiming characterAiming;
    [HideInInspector] public CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator animator;

    public Vector2[] recoilPattern;
    public float duration;
    public float recoilModifier = 1f;

    private float verticalRecoil;
    private float horizontalRecoil;
    private float time;
    private int index;

    int recoilLayerIndex = -1;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        if(animator)
        {
            recoilLayerIndex = animator.GetLayerIndex("Recoil Layer");
        }
    }

    public void Reset()
    {
        index = 0;
    }

    private int NextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex(index);
        if(animator)
        {
            animator.Play("weapon_recoil_" + weaponName, 1, 0.0f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(time > 0  && characterAiming)
        {
            characterAiming.yAxis.Value -= (((verticalRecoil / 10) * Time.deltaTime) / duration) * recoilModifier;
            characterAiming.xAxis.Value -= (((horizontalRecoil / 10) * Time.deltaTime) / duration) * recoilModifier;
            time -= Time.deltaTime;
        }
        
    }
}
