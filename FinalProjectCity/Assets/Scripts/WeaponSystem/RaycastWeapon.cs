using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        public int bounce;
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public int maxBounces = 0;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public string weaponName;
    public LayerMask layerMask;

    public int ammoCount = 30;
    public int clipSize = 30;
    public int clipCount = 2;
    public int maxAmmoCount = 90;
    public float damage = 10;

    public Transform raycastOrigin;
    public WeaponRecoil recoil;
    public GameObject magazine;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        bullet.bounce = maxBounces;
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        recoil.Reset();
    }

    public void UpdateFiring(float deltaTime, Vector3 target)
    {
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0.0f)
        {
            FireBullet(target);
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }

    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;


        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.time = maxLifeTime;
            end = hitInfo.point;
            
            //Bullet riochet
            if(bullet.bounce > 0 )
            {
                bullet.time = 0;
                bullet.initialPosition = hitInfo.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hitInfo.normal);
                bullet.bounce--;
            }

            //collision impulse
            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if(rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }


            //collision impulse
            var hitBox = hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, ray.direction);
            }
        }
        if(bullet != null && bullet.tracer != null)
        {
            bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet(Vector3 target)
    {
        if(ammoCount <= 0)
        {
            return;
        }
        ammoCount--;

        foreach (var prticale in muzzleFlash)
        {
            prticale.Emit(1);
        }

        Vector3 velocity = (target - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        recoil.GenerateRecoil(weaponName);
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        if (isFiring)
        {
            UpdateFiring(Time.deltaTime, target);
        }
        accumulatedTime += deltaTime;
        UpdateBullets(Time.deltaTime);
    }

    public bool ShouldReload()
    {
        return ammoCount == 0 && clipCount > 0;
    }

    public bool IsLowAmmo()
    {
        return ammoCount == 0 && clipCount == 0;
    }

    public void RefillAmmo()
    {
        ammoCount = clipSize;
        clipCount--;
    }
}
