using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioFire;
    [SerializeField] AudioController audioReload;

    public Transform AimTarget;
    public Vector3 AimTargetOffset;
    public bool canFire;

    float nextFireAllowe;
    Transform muzzle;
    ParticleSystem muzzleFireParticleSystem;
    WeaponRecoil m_WeaponRecoil;
    WeaponRecoil WeaponRecoil
    {
        get
        {
            if (m_WeaponRecoil == null)
                m_WeaponRecoil = GetComponent<WeaponRecoil>();

            return m_WeaponRecoil;
        }
    }


    public WeaponReloader reloader;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
    }

    public virtual void Fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowe)
            return;

        if(reloader != null)
        {
            if (reloader.IsReloading)
                return;

            if (reloader.RoundsRemainingInClip == 0)
                return;

            reloader.TakeFromClip(1);
        }

        nextFireAllowe = Time.time + rateOfFire;

        bool isLocalPlayerControlled = AimTarget == null;
        
        if(!isLocalPlayerControlled)
            muzzle.LookAt(AimTarget.position + AimTargetOffset);

        Projectile newBullet = (Projectile) Instantiate(projectile, muzzle.position, muzzle.rotation);

        if (isLocalPlayerControlled)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            Vector3 targetPosition = ray.GetPoint(500);
            if (Physics.Raycast(ray, out hit))
                targetPosition = hit.point;

            newBullet.transform.LookAt(targetPosition + AimTargetOffset);
        }

        if (this.WeaponRecoil)
            this.WeaponRecoil.Activate();

        fireEffect();

        audioFire.Play();
        canFire = true;
    }

    void fireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;

        muzzleFireParticleSystem.Play();
    }

    public void Reload()
    {
        if (reloader == null)
            return;

        reloader.Reload();
        audioReload.Play();
    }
}
